using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;
using MVCWithBlazor.Services;

namespace MVCWithBlazor.Controllers
{
    [Authorize]
    public class ProblemaController : Controller
    {
        private readonly ReportDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _env;

        public ProblemaController(ReportDbContext context, IEmailSender emailSender, IWebHostEnvironment env)
        {
            _context = context;
            _emailSender = emailSender;
            _env = env;
        }

        // GET: Problema
        public async Task<IActionResult> Index()
        {
            ViewBag.fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            ViewBag.toDate = DateTime.Now.ToString("yyyy-MM-dd");
            var reportDbContext = _context.ProblemaModels.Include(p => p.ResponsabilModel).Include(p => p.UtilajModel).OrderByDescending(data => data.DataIntroducere).Where(d => d.DataIntroducere.CompareTo(DateTime.Now.AddMonths(-1)) >= 0 && d.DataIntroducere.CompareTo(DateTime.Now) <= 0);
            // Return View for operators
            foreach (var utilaj in _context.UtilajModels)
            {
                if (User.IsInRole("Member") && User.HasClaim("Department", utilaj.UtilajModelID.ToString()))
                {
                    ViewBag.dataSource = await reportDbContext.Where(item => item.UtilajModelID == utilaj.UtilajModelID).ToListAsync();
                    return View(await reportDbContext.ToListAsync());
                }
            }
            // Return view for everybody
            ViewBag.dataSource = await reportDbContext.ToListAsync();
            return View(await reportDbContext.ToListAsync());
        }

        // POST: Problema
        [HttpPost]
        public async Task<IActionResult> Index(DateTime fromDate, DateTime toDate)
        {
            ViewBag.fromDate = fromDate.ToString("yyyy-MM-dd");
            ViewBag.toDate = toDate.ToString("yyyy-MM-dd");
            var reportDbContext = _context.ProblemaModels.Include(p => p.ResponsabilModel).Include(p => p.UtilajModel).OrderByDescending(data => data.DataIntroducere).Where(d => d.DataIntroducere.CompareTo(fromDate) >= 0 && d.DataIntroducere.CompareTo(toDate) <= 0);
            // Return View per operators
            foreach (var utilaj in _context.UtilajModels)
            {
                if (User.IsInRole("Member") && User.HasClaim("Department", utilaj.UtilajModelID.ToString()))
                {
                    ViewBag.dataSource = await reportDbContext.Where(item => item.UtilajModelID == utilaj.UtilajModelID).ToListAsync();
                    return View(await reportDbContext.ToListAsync());
                }
            }
            // return view for everybody
            ViewBag.dataSource = await reportDbContext.ToListAsync();
            return View(await reportDbContext.ToListAsync());
        }

        public IActionResult MailService()
        {
            string filePath = Path.Combine(_env.WebRootPath, "Fisiere\\MailDate.JSON");
            MailModel mailModel = _emailSender.GetMailModelAsync(filePath).Result;
            return View(mailModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MailService([Bind("FromAdress,ToAddress,Subjsect,Messaege,FilePathFisierDeTrimis")] MailModel mailModel)
        {
            string filePath = _emailSender.WriteToJsonMailData(mailModel, _env);
            //MailModel mailModel1 = _reportService.GetMailModelAsync(filePath).Result;
            return View(mailModel);
        }

        // GET: Problema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaModel = await _context.ProblemaModels
                .Include(p => p.ResponsabilModel)
                .Include(p => p.UtilajModel)
                .FirstOrDefaultAsync(m => m.ProblemaModelID == id);
            if (problemaModel == null)
            {
                return NotFound();
            }

            return View(problemaModel);
        }

        // GET: Problema/Create
        public IActionResult Create()
        {
            ViewData["ResponsabilModelID"] = new SelectList(_context.ResponsabilModel, "ResponsabilModelID", "Email");
            foreach (var utilaj in _context.UtilajModels)
            {
                if (User.IsInRole("Member") && User.HasClaim("Department", utilaj.UtilajModelID.ToString()))
                {
                    ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels.Where(t => t.UtilajModelID == utilaj.UtilajModelID).ToList(), "UtilajModelID", "Utilaj", utilaj.UtilajModelID);
                    return View();
                }
            }
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj");
            return View();
        }

        // POST: Problema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProblemaModelID,DataIntroducere,UtilajModelID,ProblemaDescriere,ComentariuMentenanta,LastPersonUpdateRow")] ProblemaModel problemaModel)
        {
            if (ModelState.IsValid)
            {
                problemaModel.ComentariuMentenanta = "-";
                problemaModel.Stare = Status.Nerezolvat;
                _context.Add(problemaModel);
                await _context.SaveChangesAsync();

                // Send Mail
                string filePathMailModel = Path.Combine(_env.WebRootPath, "Fisiere\\MailDate.JSON");
                MailModel mailModel = _emailSender.GetMailModelAsync(filePathMailModel).Result;
                UtilajModel utilajName = _context.UtilajModels.FirstOrDefault(utilaj => utilaj.UtilajModelID == problemaModel.UtilajModelID);
                await _emailSender.SendEmailAsync(mailModel.FromAdress, mailModel.ToAddress, mailModel.Subjsect, problemaModel.LastPersonUpdateRow + mailModel.Messaege + problemaModel.ProblemaDescriere + $" la utilajul: {utilajName.Utilaj}");

                return RedirectToAction(nameof(Index));
            }
            ViewData["ResponsabilModelID"] = new SelectList(_context.ResponsabilModel, "ResponsabilModelID", "Email", problemaModel.ResponsabilModelID);
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", problemaModel.UtilajModelID);
            return View(problemaModel);
        }

        // GET: Problema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaModel = await _context.ProblemaModels.FindAsync(id);
            if (problemaModel == null)
            {
                return NotFound();
            }
            ViewData["ResponsabilModelID"] = new SelectList(_context.ResponsabilModel, "ResponsabilModelID", "Email", problemaModel.ResponsabilModelID);
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", problemaModel.UtilajModelID);
            return View(problemaModel);
        }

        // POST: Problema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProblemaModelID,DataIntroducere,UtilajModelID,ProblemaDescriere,ComentariuMentenanta,Stare,ResponsabilModelID,TermenFinalizare,LastPersonUpdateRow")] ProblemaModel problemaModel)
        {
            if (id != problemaModel.ProblemaModelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Daca completeaza user Mentenanta se pune automat in lucru
                    if (User.IsInRole("Mentenanta") && problemaModel.Stare != Status.Rezolvat) 
                        problemaModel.Stare = Status.InLucru;
                    // Se actualizeaza date server
                    _context.Update(problemaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaModelExists(problemaModel.ProblemaModelID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResponsabilModelID"] = new SelectList(_context.ResponsabilModel, "ResponsabilModelID", "Email", problemaModel.ResponsabilModelID);
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", problemaModel.UtilajModelID);
            return View(problemaModel);
        }

        // GET: Problema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problemaModel = await _context.ProblemaModels
                .Include(p => p.ResponsabilModel)
                .Include(p => p.UtilajModel)
                .FirstOrDefaultAsync(m => m.ProblemaModelID == id);
            if (problemaModel == null)
            {
                return NotFound();
            }

            return View(problemaModel);
        }

        // POST: Problema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problemaModel = await _context.ProblemaModels.FindAsync(id);
            _context.ProblemaModels.Remove(problemaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemaModelExists(int id)
        {
            return _context.ProblemaModels.Any(e => e.ProblemaModelID == id);
        }
    }
}
