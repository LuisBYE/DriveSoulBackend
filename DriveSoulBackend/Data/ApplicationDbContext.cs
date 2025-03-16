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
    }
}
