using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ventaTickets.Controllers
{
    public class PagoController : Controller
    {
        private readonly ventaTicketsContext _context;

        public async Task<IActionResult> agregarEntradas()
        {


        
            return RedirectToAction("Entradas", "Usuarios");




           









        }
    }
}
