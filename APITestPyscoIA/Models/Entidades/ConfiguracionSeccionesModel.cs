using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionSecciones")]
    public class ConfiguracionSeccionesModel: BaseModel
    {

        [Required] 
        public string Seccion { get; set; }
        [Required] 
        public int NumeroPreguntas { get; set; }

        [Required]
        [ForeignKey("ConfiguracionTestModel")] 
        public int IdConfiguracionesTest { get; set; }
        [Required]
        [ForeignKey("TipoSeccionesModel")] public int IdTipoSecciones { get; set; }

        [Required]
        [RegularExpression("AVG|SUM")] 
        public string FormulaAgregado { get; set; }

        public ConfiguracionTestModel ConfiguracionTest { get; set; }
        public TipoSeccionesModel TipoSecciones { get; set; }

        public ICollection<ConfiguracionPreguntasModel> BancoPreguntas { get; set; }

    }
}
