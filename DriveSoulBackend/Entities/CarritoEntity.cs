using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveSoulBackend.Entities
{
    [Table("carrito_items")]  // Nombre de la tabla en la base de datos
    public class CarritoEntity
    {
        public int Id { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }

        [Column("fecha_actualizacion")]
        public DateTime? FechaActualizacion { get; set; }
    }
}
