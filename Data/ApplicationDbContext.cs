using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }

        public static string ConnectionString { get; set; } //Cadena de conexión

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlServer(ConnectionString);
        }


        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet <ProductoPrecio> ProductosPrecios { get; set; }
        public DbSet <Imagen> Imagenes { get; set; }
        public DbSet<ProductoVariante> productoVariantes { get; set; }


    }
}
