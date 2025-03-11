using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adrian_Hernandez_Bonilla_P2_Ap1.Models
{
    public class Encuestas
    {

        [Key]
        public int EncuestaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "LA asignatura debe ser obligatoria")]
        public string Asignatura { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el monto")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public double Monto { get; set; }


        [ForeignKey("EncuestaId")]
        public ICollection<EncuestasDetalle> encuestasDetalle { get; set; } = new List<EncuestasDetalle>();


    }
}
