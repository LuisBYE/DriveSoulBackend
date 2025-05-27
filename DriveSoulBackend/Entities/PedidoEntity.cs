using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveSoulBackend.Entities
{
    [Table("pedidos")]
    public class PedidoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("importe_total")]
        public decimal ImporteTotal { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = "pendiente";

        [Column("direccion_envio")]
        public string DireccionEnvio { get; set; }

        [Column("metodo_pago")]
        public string MetodoPago { get; set; }

        [Column("estado_pago")]
        public string EstadoPago { get; set; } = "pendiente";

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column("fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        // Relación con Usuario
        public virtual Usuarios Usuario { get; set; }

        // Relación con PedidoItems
        public virtual ICollection<PedidoItemEntity> PedidoItems { get; set; }
    }
} 