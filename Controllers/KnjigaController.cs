using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GET_Biblioteka.Data;
using GET_Biblioteka.Models;

namespace GET_Biblioteka.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KnjigaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Knjiga
        public async Task<IActionResult> Index()
        {
              return View(await _context.Knjige.ToListAsync());
        }

        // GET: Knjiga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Knjige == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige
                .FirstOrDefaultAsync(m => m.KnjigaID == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // GET: Knjiga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knjiga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KnjigaID,Naziv,Pisac,Kolicina")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knjiga);
        }

        // GET: Knjiga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Knjige == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }
            return View(knjiga);
        }

        // POST: Knjiga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KnjigaID,Naziv,Pisac,Kolicina")] Knjiga knjiga)
        {
            if (id != knjiga.KnjigaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knjiga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnjigaExists(knjiga.KnjigaID))
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
            return View(knjiga);
        }

        // GET: Knjiga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Knjige == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjige
                .FirstOrDefaultAsync(m => m.KnjigaID == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // POST: Knjiga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Knjige == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Knjige'  is null.");
            }
            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga != null)
            {
                _context.Knjige.Remove(knjiga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnjigaExists(int id)
        {
          return _context.Knjige.Any(e => e.KnjigaID == id);
        }
    }
}
