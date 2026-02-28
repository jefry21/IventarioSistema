using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Data
{
    public class BaseContext: DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
    : base(options)
        {

        }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<VentaDetalle> VentaDetalle { get; set; }
        public DbSet<Usuarios> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<VentaDetalle>()
                .HasOne<Venta>()    
                .WithMany(v => v.Detalles)
                .HasForeignKey(vd => vd.IdVenta);

            // Relación VentaDetalle → Producto
            modelBuilder.Entity<VentaDetalle>()
                .HasOne<Productos>()            
                .WithMany()
                .HasForeignKey(vd => vd.ProductoId);
        }

    }
}
