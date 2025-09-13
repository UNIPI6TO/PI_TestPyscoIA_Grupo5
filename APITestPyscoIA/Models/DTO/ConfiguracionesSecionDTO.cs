using APITestPyscoIA.Models.Entidades;
using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.DTO
{
    public class ConfiguracionesSecionDTO:BaseModel
    {
        public string Seccion { get; set; }
        [Required]
        public int NumeroPreguntas { get; set; }

        public int IdConfiguracionesTest { get; set; }


        [RegularExpression("AVG|SUM")]
        public string FormulaAgregado { get; set; }


       


    }
}
