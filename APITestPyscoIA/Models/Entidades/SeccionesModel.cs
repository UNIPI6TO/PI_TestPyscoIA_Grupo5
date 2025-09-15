using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Secciones")] 
    public class SeccionesModel:BaseModel
    {
        public float? Score { get; set; }
        public string? Resultado { get; set; }
        public DateTime? FechaInicioTest { get; set; }
        public DateTime? FechaFinTest { get; set; }

        [Required]
        [ForeignKey("Evaluaciones")] 
        public int IdEvaluaciones { get; set; }
        [JsonIgnore]
        public EvaluacionesModel? Evaluaciones { get; set; }

        [Required]
        [ForeignKey("ConfiguracionSecciones")] 
        public int IdConfiguracionSecciones { get; set; }


        
        [JsonIgnore]
        public ConfiguracionSeccionesModel? ConfiguracionSecciones { get; set; }

        [JsonIgnore]
        public ICollection<PreguntasModel>? Preguntas { get; set; }

    }
}
