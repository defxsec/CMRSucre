using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CMRSucre.Models
{
    public class Certificado
    {
        [Key]
        public int CertificadoId { get; set; }
        [DisplayName("Fecha de emisión")]        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime Emision { get; set; }

        [DisplayName("Catidad")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int Cantidad { get; set; }

        //[ForeignKey("Socio")]
        [DisplayName("Nombre de socio")]
        public int SocioId { get; set; }
        
        public Socio? Socio { get; set; }       
    }
}
