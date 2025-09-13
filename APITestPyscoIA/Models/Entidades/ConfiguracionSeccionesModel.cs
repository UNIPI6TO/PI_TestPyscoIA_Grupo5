using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionesSecciones")]
    public class ConfiguracionSeccionesModel: BaseModel
    {

        [Required] 
        public string Seccion { get; set; }
        [Required] 
        public int NumeroPreguntas { get; set; }

        [Required]
        [ForeignKey("ConfiguracionTest")] 
        public int IdConfiguracionesTest { get; set; }
        
        [Required]
        [RegularExpression("AVG|SUM")] 
        public string FormulaAgregado { get; set; }

        
        public ConfiguracionTestModel? ConfiguracionTest { get; set; }
        
        public ICollection<ConfiguracionPreguntasModel>? BancoPreguntas { get; set; }



    }
}
