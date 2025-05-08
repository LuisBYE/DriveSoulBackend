using Microsoft.EntityFrameworkCore;
using DriveSoulBackend.Entities; // Asegúrate de incluir la referencia a tu clase Producto

namespace DriveSoulBackend.Data // El namespace debe ser DriveSoulBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor recibe las opciones de configuración para el contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet para la entidad Producto
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<CocheEntity> Coches { get; set; }
        public DbSet<MarcaEntity> Marcas { get; set; }
        public DbSet<ModeloEntity> Modelos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<CocheEntity>()
        //        .HasOne(c => c.Producto)  // Relación entre CocheEntity y Producto
        //        .WithMany(p => p.Coches)  // Producto puede tener muchos Coches
        //        .HasForeignKey(c => c.ProductoId);  // Relación con la clave foránea ProductoId en CocheEntity
        //}
    }
}
