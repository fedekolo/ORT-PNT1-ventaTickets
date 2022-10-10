using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ventaTickets.Models;

    public class ventaTicketsContext : DbContext
    {
        public ventaTicketsContext(DbContextOptions<ventaTicketsContext> options)
            : base(options)
        {
        }

        public DbSet<ventaTickets.Models.Entrada> Entrada { get; set; } = default!;

        public DbSet<ventaTickets.Models.Show> Show { get; set; } = default!;
}
