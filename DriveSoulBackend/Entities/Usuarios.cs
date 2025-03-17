using System.ComponentModel.DataAnnotations;
namespace DriveSoulBackend.Entities
{
    public class Usuarios
    {
        public int? Id { get; set; }  // ID del usuario

        [Required]
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }
        public string Ciudad { get; set; }

        [Required]
        public string Password { get; set; }  // 🔹 Asegúrate de que existe esta propiedad

        public string? Rol { get; set; }  // Rol del usuario (opcional)
    }

}
