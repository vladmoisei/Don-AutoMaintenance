﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;
using MVCWithBlazor.Services;

namespace MVCWithBlazor.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ReportDbContext _context;

        public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, ReportDbContext context)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _context = context;
        }
        public async Task<IActionResult> SignUp()
        {
            var model = new SignupViewModel() { Role="Member"};
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj");
            return View(model);
        }

        // Signup without confirmation email
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!(await _roleManager.RoleExistsAsync(model.Role)))
                {
                    var role = new IdentityRole { Name = model.Role };
                    var roleResult = await _roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded)
                    {
                        var errors = roleResult.Errors.Select(s => s.Description);
                        ModelState.AddModelError("Role", string.Join(",", errors));
                        ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", model.Department);
                        return View(model);
                    }
                }
                if (await _userManager.FindByEmailAsync(model.EMail) == null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.EMail,
                        UserName = model.EMail
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    // For mail confirmation
                    //user = await _userManager.FindByEmailAsync(model.EMail);
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    if (result.Succeeded)
                    {
                        // For mail confirmation
                        //var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new { userId = user.Id, @token = token });
                        //await _emailSender.SendEmailAsync("tutorialmicrosoftwebdeveloper@gmail.com", "vladmoisei@yahoo.com", "Confirm your email address", confirmationLink);
                        var claim = new Claim("Department", model.Department);
                        await _userManager.AddClaimAsync(user, claim);
                        await _userManager.AddToRoleAsync(user, model.Role);
                        return RedirectToAction("Signin");
                    }
                    ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(x => x.Description)));
                    return View(model);
                }
            }
            return View(model);
        }

        // Confirmation Email
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("Signin");
            }
            return new NotFoundResult();
        }
        public IActionResult SignIn()
        {
            return View(new SigninViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.UserName);
                    // Exp Get List of claims and and example hopw to use it
                    //var userClaims = await _userManager.GetClaimsAsync(user);
                    //if (!userClaims.Any(x=>x.Type == "Department"))
                    //{
                    //    ModelState.AddModelError("Claim", "User doesn\'t have claim department");
                    //    return View(model);
                    //}
                    return RedirectToAction("Index", "Problema");
                    if (await _userManager.IsInRoleAsync(user, "Member"))
                    {
                        return RedirectToAction("Member", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Can\'t login.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Signin");
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                if (user != null)
                {
                    await _signinManager.SignInAsync(user, isPersistent: true);
                }
                ModelState.AddModelError("ChangePassword", "S-a realizat schimbarea parolei cu succes!");
                //return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                return View(model);
            }
            ModelState.AddModelError("ChangePassword", "A aparut o eroare in schimbarea parolei!" +
                string.Join("", result.Errors.Select(x => x.Description)));
            return View(model);
        }
    }
}