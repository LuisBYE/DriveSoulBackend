namespace DriveSoulBackend.DTO.ProductosDTO
{
        // DTO ES LO QUE USAREMOS PARA ENVIAR Y RECIBIR DATOS DE LA API
        // Y
        // LA ENTIDAD ES LO QUE USAREMOS PARA MANIPULAR LOS DATOS EN LA BASE DE DATOS
        public class ListarProductoDTO
        {
            public int id { get; set; } // ID del producto
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public string Categoria { get; set; }

        }
    }
