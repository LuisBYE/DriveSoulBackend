using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveSoulBackend.Entities
{
    [Table("imagenes")]
    public class ImagenEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("coche_id")]
        public int CocheId { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Column("url")]
        public string Url { get; set; }
    }
} 