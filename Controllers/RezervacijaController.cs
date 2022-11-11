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
    public class RezervacijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacija
        public async Task<IActionResult> Index()
        {
              return View(await _context.Rezervacije.ToListAsync());
        }

        // GET: Rezervacija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervacije == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .FirstOrDefaultAsync(m => m.RezervacijaID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezervacija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervacijaID,UserId,KnjigaID")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacija);
        }

        // GET: Rezervacija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervacije == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervacijaID,UserId,KnjigaID")] Rezervacija rezervacija)
        {
            if (id != rezervacija.RezervacijaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.RezervacijaID))
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
            return View(rezervacija);
        }

        // GET: Rezervacija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervacije == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .FirstOrDefaultAsync(m => m.RezervacijaID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervacije == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rezervacije'  is null.");
            }
            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija != null)
            {
                _context.Rezervacije.Remove(rezervacija);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
          return _context.Rezervacije.Any(e => e.RezervacijaID == id);
        }
    }
}
