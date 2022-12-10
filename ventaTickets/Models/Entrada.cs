using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ventaTickets.Models
{
    public class Entrada
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int numero { get; set; }
        public DateTime Fecha { get; set; }
        public string ubicacion { get; set; } = "";
        public double precio { get; set; }
        public int showId { get; set; }
        public int UsuarioId { get; set; }
        public string sector { get; set; }
        public Show show { get; set; }
        public Entrada(int numero, string ubicacion, double precio, int showId, string sector)
            {
                this.numero = numero;
                this.precio = precio;
                this.showId = showId;
                this.ubicacion = ubicacion;
                this.sector = sector;
                this.UsuarioId = -1;
            }

    }
}
