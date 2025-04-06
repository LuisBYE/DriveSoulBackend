namespace DriveSoulBackend.Entities
{
public class Producto
{
        public int id { get; set; } 
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
        //public ICollection<CocheEntity> Coches { get; set; } // Relación uno a muchos con CocheEntity
    }
}
