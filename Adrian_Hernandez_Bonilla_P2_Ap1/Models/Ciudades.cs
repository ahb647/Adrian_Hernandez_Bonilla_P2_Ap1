using System.ComponentModel.DataAnnotations;

namespace Adrian_Hernandez_Bonilla_P2_Ap1.Models
{
    public class Ciudades
    {
        [Key]
        public int CiudadId { get; set; }
       
        public string NombreCiudad { get; set; } 


        public double Monto { get; set; }   

    }

}