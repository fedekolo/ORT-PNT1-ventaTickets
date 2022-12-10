using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Sockets;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using static QRCoder.PayloadGenerator;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ventaTickets.Migrations;
using ventaTickets.Models;
using System.Security.Claims;

namespace ventaTickets.Controllers
{


    public class MailsController : Controller
    {

        private readonly ventaTicketsContext _context;

        public MailsController(ventaTicketsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public void mandarMail(MailMessage mail)
        {
            string correoOrigen = "ventaDeTicketsPN1@gmail.com";
            string contrasenia = "muatfcgevtsmiqmh";


            mail.From = new MailAddress(correoOrigen);



            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(correoOrigen, contrasenia);

            smtp.Send(mail);


        }


        [HttpGet]
        public IActionResult enviarContrasenia()
        {
            return View("Index");
        }

        [HttpPost]

        public IActionResult enviarContrasenia(string email)
        {
            var usuario = _context.Usuario.Where(e => e.email == email).FirstOrDefault();

            if (usuario != null)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(email));
                mail.Body = "La contraseña de la cuenta es: " + usuario.password;
                mandarMail(mail);
            }

            return RedirectToAction("Index", "Home");

        }




        [HttpPost]
        public IActionResult confirmacionPago(string nomCompleto,string dni,string numTarjeta)
        {
            int id = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _context.Usuario.Where(e => e.Id == id).FirstOrDefault();

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(usuario.email));
            mail.Body = "La Compra se realizo con exito, Pago a nombre de " + nomCompleto + " dni "+ dni + " con numero de tarjeta: " + numTarjeta;

            mandarMail(mail);

            return RedirectToAction("Index", "Home");


        }






    }




}