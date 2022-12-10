using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ventaTickets.Models
{
    public class Show
    {
        [Key]
        public int showId { get; set; }
        [Required]

        public string nombre { get; set; } = "";
        public string descripcion { get; set; } = "";
        public string direccion { get; set; } = "";
        public string imagen { get; set; } = "";
        public int cantCampo { get; set; }
        public double precioCampo { get; set; }
        public int cantPlateaBaja { get; set; } 
        public double precioPlateaBaja { get; set; }
        public int cantPlateaAlta { get; set; }
        public int precioPlateaAlta { get; set; }
        public DateTime fecha { get; set; }
        public ICollection<Entrada> entradas { get; set; } = new List<Entrada>();

        




    }







}
