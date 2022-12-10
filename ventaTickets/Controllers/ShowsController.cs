using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using QRCoder;
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

        private async void editarEntradas(Show show)
        {
            var entradas = _context.Entrada.Where(e => e.showId == show.showId);

            show.entradas.Clear();

            entradas.ExecuteDelete();

            generarEntradas(show);

            await _context.SaveChangesAsync();
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

            ViewBag.entradasCampos = cantidadPorSector("Campo", id) ;
            ViewBag.entradasPA = cantidadPorSector("Platea Alta",id) ;
            ViewBag.entradasPB = cantidadPorSector("Platea Baja",id) ;




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
            if (hayCantidad(id, sector, cantidad))
            {

            
            int idUsuario = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            var show = await _context.Show
              .FirstOrDefaultAsync(m => m.showId == id);


            double precio = calcularPrecio(show, sector, cantidad);
            ViewBag.Cantidad = cantidad;
            ViewBag.Sector = sector;
            ViewBag.Precio = precio;

            for (int i = 0; i < cantidad; i++)
            {
                var entrada = _context.Entrada.Where(e => e.UsuarioId == -1 && e.sector == sector && e.showId == show.showId).FirstOrDefault();
                entrada.UsuarioId = idUsuario;
                _context.Entrada.Update(entrada);
                _context.SaveChanges(); 
            } 

            return View(show);
            }
            else
            {
                return View("NoHayEntradas");
            }
        }

        [Authorize(Roles = "Usuario")]
        // GET: Shows/DetailsEntrada/5
        public async Task<IActionResult> DetailsEntrada(int? id)
        {

             var entrada = await _context.Entrada.FirstOrDefaultAsync(e => e.Id ==id);

            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show.FirstOrDefaultAsync(m => m.showId == entrada.showId);

            if (show == null)
            {
                return NotFound();
            }

            int idUsuario = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _context.Usuario.Where(e => e.Id == idUsuario).FirstOrDefault();
             
            string idQr = "idEntrada"+id.ToString()+"-ShowId" + show.showId.ToString() + "-" + usuario.email ;


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(idQr, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qRCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            string model = Convert.ToBase64String(qrCodeImage);


            ViewBag.qrCodeImage = model;


            return View(show);
        }

        private int cantidadPorSector(string sector, int? id)
        {

            int contador = _context.Entrada.Where(e => e.UsuarioId == -1 && e.sector == sector && e.showId == id).Count();

            return contador;
        }
        private Boolean hayCantidad(int? id, string sector, int cantidad)
        {

            int contador = _context.Entrada.Where(e => e.UsuarioId == -1 && e.sector == sector && e.showId == id).Count();

            return contador>= cantidad;
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







        //metodo para ver las cantidad de entradas vendidas
        public async Task<IActionResult> Index3()
        {
            var shows = await _context.Show.ToListAsync();
            int contador = 0;
            int[] vectorCantVendidas = new int[shows.Count];
            int[] vectorCantDisponibles = new int[shows.Count];
            int[] vectorRestan = new int[shows.Count];
            double[]vectorTotalRecaudado = new double[shows.Count];
            foreach (var item in shows)
            {
                vectorCantVendidas[contador] = cantidadEntradasVendidas(item);
                vectorCantDisponibles[contador] = cantidadEntradasDisponibles(item);
                vectorRestan[contador] =  vectorCantVendidas[contador] + vectorCantDisponibles[contador];
                vectorTotalRecaudado[contador] = calcularTotalVendido(item);
                contador++;
            }


            ViewBag.cantEntradasVendidas = vectorCantVendidas;
            ViewBag.cantEntradasDisponiles = vectorCantDisponibles;
            ViewBag.disponibles = vectorRestan;
            ViewBag.totalRecaudado = vectorTotalRecaudado;

            return View(shows);
        }

        private double calcularTotalVendido(Show show)
        {   
            double total = 0;
            var entradas = _context.Entrada.Where(e => e.UsuarioId != -1 && e.showId == show.showId);
            foreach (var item in entradas)
            {
                total += item.precio;
            }


            return total;
        }


        private int cantidadEntradasVendidas(Show show)
        {
            int contador = 0;

            contador = _context.Entrada.Where(e => e.UsuarioId != -1 &&  e.showId == show.showId).Count();

            return contador;
        }

        private int cantidadEntradasDisponibles(Show show)
        {
            int contador = 0;

            contador = _context.Entrada.Where(e => e.UsuarioId == -1 && e.showId == show.showId).Count();

            return contador;
        }







    }
    
}
