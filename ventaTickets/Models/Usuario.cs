using System.ComponentModel.DataAnnotations;

namespace ventaTickets.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; } = "";

        public string dni { get; set; } = "";
        [Required]
        public string email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public Boolean administrador { get; set; }
    }
}
