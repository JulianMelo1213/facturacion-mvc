using Microsoft.EntityFrameworkCore;
using Prueba_Técnica_Factura.Models;


namespace Prueba_Técnica_Factura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFactura");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Producto>().ToTable("Producto");

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.Detalles)
                .WithOne(d => d.Factura)
                .HasForeignKey(d => d.FacturaID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
