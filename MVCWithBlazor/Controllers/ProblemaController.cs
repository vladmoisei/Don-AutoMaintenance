using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;

namespace MVCWithBlazor.Controllers
{
    [Authorize]
    public class ProblemaController : Controller
    {
        private readonly ReportDbContext _context;

        public ProblemaController(ReportDbContext context)
        {
            _context = context;
        }

        // GET: Problema
        public async Task<IActionResult> Index()
        {
            var reportDbContext = _context.ProblemaModels.Include(p => p.ResponsabilModel).Include(p => p.UtilajModel);
            return View(await reportDbContext.ToListAsync());
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
                problemaModel.ComentariuMentenanta = "";
                problemaModel.Stare = Status.Nerezolvat;
                _context.Add(problemaModel);
                await _context.SaveChangesAsync();
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
