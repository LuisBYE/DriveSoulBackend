namespace DriveSoulBackend.DTO.CochesDTO
{
	public class ListarProductoCocheDTO
	{
		//Datos de PRODUCTO
		public string nombre { get; set; }
		public string descripcion { get; set; }
		public decimal precio { get; set; }
		public string categoria { get; set; }

		//Datos de COCHES
		public int producto_id { get; set; }
		public int modelo_id { get; set; }
		public int anio { get; set; }
		public int kilometraje { get; set; }
		public string color { get; set; }
		public string tipo_combustible { get; set; }
		public string transmision { get; set; }

		public string modeloNombre { get; set; }
        public string marcaNombre { get; set; }

    }
}
