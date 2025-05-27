using System;
using System.Collections.Generic;

namespace DriveSoulBackend.DTO.PedidosDTO
{
    public class ListarPedidoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; }
        public string DireccionEnvio { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public List<PedidoItemDTO> Items { get; set; } = new List<PedidoItemDTO>();
    }

    public class PedidoItemDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
} 