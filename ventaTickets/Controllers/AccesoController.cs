using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using ventaTickets.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Validacion_cookies.Controllers;

namespace ventaTickets.Controllers
{
    public class AccesoController : BaseController
    {

        private readonly ventaTicketsContext _context;

        public AccesoController(ventaTicketsContext context) : base(context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {

            return await this.Login(_usuario);

        }




        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Index", "Home");
        }



        public IActionResult registrarse()
        {
            return RedirectToAction("Create", "Usuarios");
        }


    }
}
