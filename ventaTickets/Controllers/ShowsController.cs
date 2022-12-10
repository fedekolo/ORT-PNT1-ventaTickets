using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ventaTickets.Migrations;
using ventaTickets.Models;

namespace ventaTickets.Controllers
{
    public class ShowsController : Controller
    {
        private readonly ventaTicketsContext _context;

        public ShowsController(ventaTicketsContext context)
        {
            _context = context;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
              return View(await _context.Show.ToListAsync());
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.showId == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Show show)
        {
            if (ModelState.IsValid)
            {
                generarEntradas(show);
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("showId,nombre,descripcion,direccion,imagen,cantCampo,precioCampo,cantPlateaBaja,precioPlateaBaja,cantPlateaAlta,precioPlateaAlta,fecha")] Show show)
        {
            if (id != show.showId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    editarEntradas(show);
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.showId))
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
            return View(show);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.showId == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Show == null)
            {
                return Problem("Entity set 'ventaTicketsContext.Show'  is null.");
            }
            var show = await _context.Show.FindAsync(id);
            if (show != null)
            {
                _context.Show.Remove(show);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
          return _context.Show.Any(e => e.showId == id);
        }

        private void generarEntradas(Show show)
        {
            for (int i = 0; i < show.cantCampo; i++)
            {
                Entrada entrada = new Entrada(i+1, show.direccion, show.precioCampo, show.showId, "Campo");
                show.entradas.Add(entrada);
            }
            for (int i = 0; i < show.cantPlateaAlta; i++)
            {
                Entrada entrada = new Entrada(i + 1, show.direccion, show.precioPlateaAlta, show.showId, "Platea Alta");
                show.entradas.Add(entrada);
            }
            for (int i = 0; i < show.cantPlateaBaja; i++)
            {
                Entrada entrada = new Entrada(i + 1, show.direccion, show.precioPlateaBaja, show.showId, "Platea Baja");
                show.entradas.Add(entrada);
            }
        }

        private void editarEntradas(Show show)
        {
            show.entradas.Clear();

            generarEntradas(show);
        }

        // GET: Index2
        public async Task<IActionResult> Index2()
        {
            return View(await _context.Show.ToListAsync());
        }

        [Authorize(Roles = "Usuario")]
        // GET: Shows/VistaCompra/5
        public async Task<IActionResult> VistaCompra(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.showId == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        [Authorize(Roles = "Usuario")]
        // GET: Shows/Pago/5
        public async Task<IActionResult> Pago(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.showId == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        [HttpPost]
        public async Task<IActionResult> Pago(int? id,string sector, int cantidad)
        {
            int idUsuario = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var show = await _context.Show
              .FirstOrDefaultAsync(m => m.showId == id);

            ViewBag.Cantidad = cantidad;
            ViewBag.Sector = sector;
            ViewBag.Precio = calcularPrecio(show, sector, cantidad);

            for (int i = 0; i < cantidad; i++)
            {
                var entrada = _context.Entrada.Where(e => e.UsuarioId == -1 && e.sector == sector && e.showId == show.showId).FirstOrDefault();
                entrada.UsuarioId = idUsuario;
                _context.Entrada.Update(entrada);
                _context.SaveChanges();
            }
            return View(show);

        }
        private Boolean hayCantidad(int cantidad, string sector)
        { 

            contador = _context.Entrada.Where(e => e.UsuarioId == -1 && e.sector == sector).Count();

            return contador >= cantidad;
        }

        private double calcularPrecio(Show show, string sector, int cantidad)
        {
            double precioTotal = 0;

            if (sector == "Campo")
            {
                precioTotal = cantidad * show.precioCampo;
            }
            else if (sector == "Platea Baja")
            {
                precioTotal = cantidad * show.precioPlateaBaja;
            }
            else
            {
                precioTotal = cantidad * show.precioPlateaAlta;
            }

            return precioTotal;
        }

    }

}
