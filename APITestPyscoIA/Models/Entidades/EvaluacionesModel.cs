using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Evaluaciones")]
    public class EvaluacionesModel : BaseModel  
    {
        public int CantidadPreguntas { get; set; }

        [Required]
        [ForeignKey("ConfiguracionTest")] 
        public int IdConfiguracionTest { get; set; }
        [Required]
        [ForeignKey("Evaluador")] 
        public int IdEvaluador { get; set; }
        [Required]
        [ForeignKey("Paciente")] 
        public int IdPaciente { get; set; }

        [Required]
        public string Evaluacion { get; set; }
        public int? Duracion { get; set; }
        public int? Contestadas { get; set; }
        public int? NoContestadas { get; set; }
        public bool? Completado { get; set; }
        public bool? Iniciado { get; set; }
        
        public int? TiempoTranscurrido { get; set; }
        public DateTime? FechaInicioTest { get; set; }
        public DateTime? FechaFinTest { get; set; }


        [JsonIgnore]
        public ConfiguracionTestModel? ConfiguracionTest { get; set; }

        [JsonIgnore]
        public EvaluadorModel? Evaluador { get; set; }

        [JsonIgnore]
        public PacienteModel? Paciente { get; set; }

       
        public ICollection<SeccionesModel>? Secciones { get; set; }
    }
}
