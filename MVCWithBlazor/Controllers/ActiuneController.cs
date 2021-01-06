using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Controllers
{
    [Authorize]
    public class ActiuneController : Controller
    {
        private readonly ReportDbContext _context;

        public ActiuneController(ReportDbContext context)
        {
            _context = context;
        }
        // GET: ActiuneController
        public ActionResult Index()
        {
            ViewBag.UtilajModelID = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj");
            return View(_context.ActiuneModels.Include(o => o.UtilajModel).ToList());
        }

        // POST: ActiuneController
        [HttpPost]
        public ActionResult Index(int idUtilaj, TipActiune tipActiune)
        {
            ViewBag.UtilajModelID = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", idUtilaj);
            ViewBag.tipActiune = tipActiune;
            List<ActiuneModel> listaActiune;
            if (tipActiune != TipActiune.Toate)
            {
                listaActiune = _context.ActiuneModels.Include(o => o.UtilajModel).Where(obj => obj.UtilajModelID == idUtilaj && obj.Tip == tipActiune).ToList();
            } else listaActiune = _context.ActiuneModels.Include(o => o.UtilajModel).Where(obj => obj.UtilajModelID == idUtilaj).ToList();
            return View(listaActiune);
        }

        // GET: ActiuneController/Create
        public ActionResult Create()
        {
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj");
            return View();
        }

        // POST: ActiuneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActiuneModelID,Descriere,Tip,IsUseInputText,InputText,UtilajModelID")] ActiuneModel actiuneModel)
        {
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", actiuneModel.UtilajModelID);
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(actiuneModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(actiuneModel);
            }
            return View(actiuneModel);
        }

        // GET: ActiuneController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiuneModel = await _context.ActiuneModels.FindAsync(id);
            if (actiuneModel == null)
            {
                return NotFound();
            }
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", actiuneModel.UtilajModelID);
            return View(actiuneModel);
        }

        // POST: ActiuneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("ActiuneModelID,Descriere,Tip,IsUseInputText,InputText,UtilajModelID")] ActiuneModel actiuneModel)
        {
            if (id != actiuneModel.ActiuneModelID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actiuneModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiuneModelExists(actiuneModel.ActiuneModelID))
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
            ViewData["UtilajModelID"] = new SelectList(_context.UtilajModels, "UtilajModelID", "Utilaj", actiuneModel.UtilajModelID);
            return View(actiuneModel);
        }

        // GET: ActiuneController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilajModel = await _context.ActiuneModels.Include(o => o.UtilajModel)
                .FirstOrDefaultAsync(m => m.ActiuneModelID == id);
            if (utilajModel == null)
            {
                return NotFound();
            }

            return View(utilajModel);
        }

        // POST: Utilaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var utilajModel = await _context.ActiuneModels.FindAsync(id);
            _context.ActiuneModels.Remove(utilajModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Utilaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiuneModel = await _context.ActiuneModels
                .FirstOrDefaultAsync(m => m.ActiuneModelID == id);
            if (actiuneModel == null)
            {
                return NotFound();
            }

            return View(actiuneModel);
        }

        private bool ActiuneModelExists(int id)
        {
            return _context.ActiuneModels.Any(e => e.ActiuneModelID == id);
        }
    }
}
