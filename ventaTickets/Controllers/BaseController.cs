using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ventaTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace Validacion_cookies.Controllers
{
    public class BaseController : Controller
    {
        private readonly ventaTicketsContext _context;

        public BaseController(ventaTicketsContext context)
        {
            _context = context;
        }
        public Usuario validarUsuario(string email, string password)
        {
            return _context.Usuario.FirstOrDefault(item => item.email == email && item.password == password);
        }
        public async Task<IActionResult> Login(Usuario _usuario)
        {
            
            var usuario = validarUsuario(_usuario.email, _usuario.password);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.nombre),
                    new Claim("email", usuario.email),
                    new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                };

                if (usuario.administrador)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Usuario"));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {               
                return View();
            }

            

        }
    }
}
