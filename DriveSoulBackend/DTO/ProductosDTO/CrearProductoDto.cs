using System;

public class CrearProductoDto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Categoria { get; set; }
    
    // ESTA POR MIRAR QUE HACER CON ESTOS CAMPOS MAS organizado 
    //public int Stock { get; set; }
    //public string ImagenUrl { get; set; }
    //public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    //public bool Activo { get; set; } = true;
    //public int IdUsuarioCreador { get; set; }

    //public string Nombre { get; set; }
    //public string Descripcion { get; set; }
    //public decimal Precio { get; set; }
    //public string Categoria { get; set; }
}
