using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace netKubernetes.Models
{
    public class Inmueble
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }

        [Required]
        [Column(TypeName = "Decimal(18,4)")]
        public decimal Precio { get; set; }
        public string? Picture { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
