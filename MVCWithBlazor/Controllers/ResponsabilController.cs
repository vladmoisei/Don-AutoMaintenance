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
    public class ResponsabilController : Controller
    {
        private readonly ReportDbContext _context;

        public ResponsabilController(ReportDbContext context)
        {
            _context = context;
        }

        // GET: Responsabil
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResponsabilModel.ToListAsync());
        }

        // GET: Responsabil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsabilModel = await _context.ResponsabilModel
                .FirstOrDefaultAsync(m => m.ResponsabilModelID == id);
            if (responsabilModel == null)
            {
                return NotFound();
            }

            return View(responsabilModel);
        }

        // GET: Responsabil/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Responsabil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResponsabilModelID,Nume,Prenume,Email,Functie,Departament")] ResponsabilModel responsabilModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsabilModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsabilModel);
        }

        // GET: Responsabil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsabilModel = await _context.ResponsabilModel.FindAsync(id);
            if (responsabilModel == null)
            {
                return NotFound();
            }
            return View(responsabilModel);
        }

        // POST: Responsabil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResponsabilModelID,Nume,Prenume,Email,Functie,Departament")] ResponsabilModel responsabilModel)
        {
            if (id != responsabilModel.ResponsabilModelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsabilModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsabilModelExists(responsabilModel.ResponsabilModelID))
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
            return View(responsabilModel);
        }

        // GET: Responsabil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsabilModel = await _context.ResponsabilModel
                .FirstOrDefaultAsync(m => m.ResponsabilModelID == id);
            if (responsabilModel == null)
            {
                return NotFound();
            }

            return View(responsabilModel);
        }

        // POST: Responsabil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsabilModel = await _context.ResponsabilModel.FindAsync(id);
            _context.ResponsabilModel.Remove(responsabilModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsabilModelExists(int id)
        {
            return _context.ResponsabilModel.Any(e => e.ResponsabilModelID == id);
        }
    }
}
