using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Frontend.Data;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class VaerktoejController : Controller
    {
        private readonly ApplicationDbContextFrontend _context;

        public VaerktoejController(ApplicationDbContextFrontend context)
        {
            _context = context;
        }

        // GET: Vaerktoej
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vaerktoej.ToListAsync());
        }

        // GET: Vaerktoej/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .FirstOrDefaultAsync(m => m.VTId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // GET: Vaerktoej/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaerktoej/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VTId,VTAnskaffet,VTFabrikat,VTModel,VTSerienr,VTType,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoej);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaerktoej);
        }

        // GET: Vaerktoej/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            if (vaerktoej == null)
            {
                return NotFound();
            }
            return View(vaerktoej);
        }

        // POST: Vaerktoej/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VTId,VTAnskaffet,VTFabrikat,VTModel,VTSerienr,VTType,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (id != vaerktoej.VTId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaerktoej);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaerktoejExists(vaerktoej.VTId))
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
            return View(vaerktoej);
        }

        // GET: Vaerktoej/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .FirstOrDefaultAsync(m => m.VTId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // POST: Vaerktoej/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            _context.Vaerktoej.Remove(vaerktoej);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaerktoejExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VTId == id);
        }
    }
}
