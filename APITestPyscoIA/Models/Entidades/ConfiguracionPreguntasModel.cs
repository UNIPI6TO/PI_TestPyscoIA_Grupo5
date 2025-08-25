using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionesPreguntas")]
    public class ConfiguracionPreguntasModel:BaseModel
    {

        [Required] 
        public string Pregunta { get; set; }
        [Required]
        [ForeignKey("ConfiguracionSecciones")]
        public int IdConfiguracionSecciones { get; set; }

        public ConfiguracionSeccionesModel ConfiguracionSecciones { get; set; } 
        public ICollection<ConfiguracionOpcionesModel> Opciones { get; set; }

    }
}
