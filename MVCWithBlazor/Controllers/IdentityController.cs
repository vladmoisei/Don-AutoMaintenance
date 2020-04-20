using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCWithBlazor.Models;
using MVCWithBlazor.Services;

namespace MVCWithBlazor.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public IdentityController(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager; 
            _emailSender = emailSender;
        }
        public async Task<IActionResult> SignUp()
        {
            var model = new SignupViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.EMail) == null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.EMail,
                        UserName = model.EMail
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    user = await _userManager.FindByEmailAsync(model.EMail);

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (result.Succeeded)
                    {
                        var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new { userId = user.Id, @token = token });
                        await _emailSender.SendEmailAsync("tutorialmicrosoftwebdeveloper@gmail.com", "vladmoisei@yahoo.com", "Confirm your email address", confirmationLink);
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
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}