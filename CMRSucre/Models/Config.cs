using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMRSucre.Models
{
    public class Config
    {
        [Key]
        public int ConfigId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(14, 2)")]
        [Precision(14, 2)]
        [DisplayName("Utilidad")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public decimal Utilidad { get; set; }

        [DisplayName("Fecha de cierre")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime Fecha_Cierre { get; set; }
    }
}
