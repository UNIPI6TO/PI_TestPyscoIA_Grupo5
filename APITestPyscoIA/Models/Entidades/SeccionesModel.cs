using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Secciones")] 
    public class SeccionesModel:BaseModel
    {
        public SeccionesModel()
        {
            Score = 0;
            Seccion = String.Empty;
            IdEvaluaciones = 0;
            Evaluaciones = null;
            IdConfiguracionSecciones = 0;
            ConfiguracionSecciones = null;
            Preguntas =  new List<PreguntasModel>();
            FormulaAgregado = "SUM";
            base.Id = 0;
            base.Creado = DateTime.Now;
            base.Eliminado = false;
        }
        public float? Score { get; set; }
        
        public  string? Resultado{ get; set; }

        [Required]
        public string Seccion { get; set; }

        [Required]
        [ForeignKey("Evaluaciones")] 
        public int IdEvaluaciones { get; set; }
        [JsonIgnore]
        public EvaluacionesModel? Evaluaciones { get; set; }

        [Required]
        [RegularExpression("AVG|SUM")]
        public string FormulaAgregado { get; set; }

        [Required]
        [ForeignKey("ConfiguracionSecciones")] 
        public int IdConfiguracionSecciones { get; set; }


        
        [JsonIgnore]
        public ConfiguracionSeccionesModel? ConfiguracionSecciones { get; set; }

        public ICollection<PreguntasModel>? Preguntas { get; set; }

    }
}
