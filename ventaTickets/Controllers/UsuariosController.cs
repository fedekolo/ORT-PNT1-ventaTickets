using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Validacion_cookies.Controllers;
using ventaTickets.Models;

namespace ventaTickets.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : BaseController
    {
        private readonly ventaTicketsContext _context;

        public UsuariosController(ventaTicketsContext context) : base(context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.Usuario.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details()
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            int id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));



            //if (id == null || _context.Usuario == null)
            //{
            //    return NotFound();
            //}

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        [AllowAnonymous]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,dni,email,password,administrador")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                if (!UsuarioExistsEmail(usuario.email))
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return await this.Login(usuario);
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(usuario);
                }


            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,dni,email,password,administrador")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (!UsuarioExistsEmail(id, usuario.email))
                    {
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return View(usuario);
                    }



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'ventaTicketsContext.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return _context.Usuario.Any(e => e.Id == id);
        }

        private bool UsuarioExistsEmail(string email)
        {
            return _context.Usuario.Any(e => e.email == email);
        }

        private bool UsuarioExistsEmail(int id, string email)
        {
            return _context.Usuario.Any(e => e.email == email && e.Id != id);
        }

        private bool UsuarioYPasswordValidas(string email, string password)
        {
            return _context.Usuario.Any(e => e.email == email && e.password == password);
        }


    }
}
