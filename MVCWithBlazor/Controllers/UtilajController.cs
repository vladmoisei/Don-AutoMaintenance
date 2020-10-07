using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;

namespace MVCWithBlazor.Controllers
{
    public class UtilajController : Controller
    {
        private readonly ReportDbContext _context;

        public UtilajController(ReportDbContext context)
        {
            _context = context;
        }

        // GET: Utilaj
        public async Task<IActionResult> Index()
        {
            return View(await _context.UtilajModels.ToListAsync());
        }

        // GET: Utilaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilajModel = await _context.UtilajModels
                .FirstOrDefaultAsync(m => m.UtilajModelID == id);
            if (utilajModel == null)
            {
                return NotFound();
            }

            return View(utilajModel);
        }

        // GET: Utilaj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilajModelID,Utilaj,ZonaUtilaj")] UtilajModel utilajModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilajModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilajModel);
        }

        // GET: Utilaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilajModel = await _context.UtilajModels.FindAsync(id);
            if (utilajModel == null)
            {
                return NotFound();
            }
            return View(utilajModel);
        }

        // POST: Utilaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilajModelID,Utilaj,ZonaUtilaj")] UtilajModel utilajModel)
        {
            if (id != utilajModel.UtilajModelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilajModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilajModelExists(utilajModel.UtilajModelID))
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
            return View(utilajModel);
        }

        // GET: Utilaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilajModel = await _context.UtilajModels
                .FirstOrDefaultAsync(m => m.UtilajModelID == id);
            if (utilajModel == null)
            {
                return NotFound();
            }

            return View(utilajModel);
        }

        // POST: Utilaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilajModel = await _context.UtilajModels.FindAsync(id);
            _context.UtilajModels.Remove(utilajModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilajModelExists(int id)
        {
            return _context.UtilajModels.Any(e => e.UtilajModelID == id);
        }
    }
}
