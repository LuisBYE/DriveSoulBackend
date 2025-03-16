namespace DriveSoulBackend.Entities
{
    public class Usuarios 
    {
        public int? Id { get; set; } // ID del usuario (clave primaria)
        public string Nombre { get; set; } // Nombre del usuario
        public string Apellido { get; set; } // Apellido del usuario
        public string Email { get; set; } // Correo del usuario
        public string Telefono { get; set; } // Teléfono del usuario
        public string Ciudad { get; set; } // Ciudad del usuario
        public string password { get; set; } // Contraseña del usuario
        public string? Rol { get; set; } // Rol del usuario (opcional)
    }
}
