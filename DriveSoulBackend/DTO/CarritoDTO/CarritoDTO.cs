namespace DriveSoulBackend.DTO.CarritoDTO
{
	public class CarritoDTO
	{
		public int Id { get; set; }
		public int UsuarioId { get; set; }
		public int ProductoId { get; set; }  // Cambio aquí
		public int Cantidad { get; set; }
		public DateTime? FechaCreacion { get; set; } = DateTime.UtcNow;
		public DateTime? FechaActualizacion { get; set; }
	}
}
