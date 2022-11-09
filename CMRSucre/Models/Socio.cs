using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMRSucre.Models
{
    public class Socio
    {
        [Key]
        public int SocioId { get; set; }
        [DisplayName("Nombre del socio")]
        [MaxLength(90)]        
        [Column(TypeName = "nvarchar(90)")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string? Nombre { get; set; }

        public List<Certificado>? Certificados { get; set; }
    }
}