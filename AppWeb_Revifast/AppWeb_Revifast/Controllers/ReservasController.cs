using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppWeb_Revifast.Models;

namespace AppWeb_Revifast.Controllers
{
    public class ReservasController : Controller
    {
        private readonly DbRevifastContext _context;

        public ReservasController(DbRevifastContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var dbRevifastContext = _context.Reserva.Include(r => r.IdAfiliadoNavigation).Include(r => r.IdConductorNavigation);
            return View(await dbRevifastContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.IdAfiliadoNavigation)
                .Include(r => r.IdConductorNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["IdAfiliado"] = new SelectList(_context.Afiliado, "IdAfiliado", "IdAfiliado");
            ViewData["IdConductor"] = new SelectList(_context.Conductor, "IdConductor", "Apellido");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,IdConductor,IdAfiliado,Fecha")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAfiliado"] = new SelectList(_context.Afiliado, "IdAfiliado", "IdAfiliado", reserva.IdAfiliado);
            ViewData["IdConductor"] = new SelectList(_context.Conductor, "IdConductor", "Apellido", reserva.IdConductor);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdAfiliado"] = new SelectList(_context.Afiliado, "IdAfiliado", "IdAfiliado", reserva.IdAfiliado);
            ViewData["IdConductor"] = new SelectList(_context.Conductor, "IdConductor", "Apellido", reserva.IdConductor);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdConductor,IdAfiliado,Fecha")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
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
            ViewData["IdAfiliado"] = new SelectList(_context.Afiliado, "IdAfiliado", "IdAfiliado", reserva.IdAfiliado);
            ViewData["IdConductor"] = new SelectList(_context.Conductor, "IdConductor", "Apellido", reserva.IdConductor);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.IdAfiliadoNavigation)
                .Include(r => r.IdConductorNavigation)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.IdReserva == id);
        }
    }
}
