using System.ComponentModel.DataAnnotations;

namespace ventaTickets.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string mail { get; set; } = "";
        public int dni { get; set; }
        public string nombre { get; set; } = "";
        public string contrasena { get; set; } = "";
        public Boolean administrador { get; set; }

    }
}
