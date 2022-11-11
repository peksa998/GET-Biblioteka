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
    public class IznajmljenaKnjigaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IznajmljenaKnjigaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IznajmljenaKnjiga
        public async Task<IActionResult> Index()
        {
              return View(await _context.IznajmljeneKnjige.ToListAsync());
        }

        // GET: IznajmljenaKnjiga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IznajmljeneKnjige == null)
            {
                return NotFound();
            }

            var iznajmljenaKnjiga = await _context.IznajmljeneKnjige
                .FirstOrDefaultAsync(m => m.IznajmljenaKnjigaID == id);
            if (iznajmljenaKnjiga == null)
            {
                return NotFound();
            }

            return View(iznajmljenaKnjiga);
        }

        // GET: IznajmljenaKnjiga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IznajmljenaKnjiga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IznajmljenaKnjigaID,KnjigaID,UserId,DatumVracanja")] IznajmljenaKnjiga iznajmljenaKnjiga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iznajmljenaKnjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iznajmljenaKnjiga);
        }

        // GET: IznajmljenaKnjiga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IznajmljeneKnjige == null)
            {
                return NotFound();
            }

            var iznajmljenaKnjiga = await _context.IznajmljeneKnjige.FindAsync(id);
            if (iznajmljenaKnjiga == null)
            {
                return NotFound();
            }
            return View(iznajmljenaKnjiga);
        }

        // POST: IznajmljenaKnjiga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IznajmljenaKnjigaID,KnjigaID,UserId,DatumVracanja")] IznajmljenaKnjiga iznajmljenaKnjiga)
        {
            if (id != iznajmljenaKnjiga.IznajmljenaKnjigaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iznajmljenaKnjiga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IznajmljenaKnjigaExists(iznajmljenaKnjiga.IznajmljenaKnjigaID))
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
            return View(iznajmljenaKnjiga);
        }

        // GET: IznajmljenaKnjiga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IznajmljeneKnjige == null)
            {
                return NotFound();
            }

            var iznajmljenaKnjiga = await _context.IznajmljeneKnjige
                .FirstOrDefaultAsync(m => m.IznajmljenaKnjigaID == id);
            if (iznajmljenaKnjiga == null)
            {
                return NotFound();
            }

            return View(iznajmljenaKnjiga);
        }

        // POST: IznajmljenaKnjiga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IznajmljeneKnjige == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IznajmljeneKnjige'  is null.");
            }
            var iznajmljenaKnjiga = await _context.IznajmljeneKnjige.FindAsync(id);
            if (iznajmljenaKnjiga != null)
            {
                _context.IznajmljeneKnjige.Remove(iznajmljenaKnjiga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IznajmljenaKnjigaExists(int id)
        {
          return _context.IznajmljeneKnjige.Any(e => e.IznajmljenaKnjigaID == id);
        }
    }
}
