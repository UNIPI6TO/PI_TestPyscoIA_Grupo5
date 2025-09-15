using APITestPyscoIA.Models.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Preguntas")]
    public class PreguntasModel: BaseModel  
    {

        [Required] public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        [Precision(8, 4)]
        public decimal Valor { get; set; }

        [Required][ForeignKey("ConfiguracionPreguntas")] 
        public int IdConfiguracionPreguntas { get; set; }
        
        [Required][ForeignKey("Secciones")] 
        public int IdSecciones { get; set; }
        [JsonIgnore]
        public SeccionesModel? Secciones { get; set; }

        [JsonIgnore]
        public ConfiguracionPreguntasModel? ConfiguracionPreguntas { get; set; }
        
        
    }
}
