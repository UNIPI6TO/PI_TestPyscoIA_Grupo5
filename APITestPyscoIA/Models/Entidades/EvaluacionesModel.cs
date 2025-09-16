using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Evaluaciones")]
    public class EvaluacionesModel : BaseModel  
    {
        public EvaluacionesModel()
        {
            CantidadPreguntas = 0;
            IdConfiguracionTest = 0;
            IdEvaluador = 0;
            IdPaciente = 0;
            Evaluacion = string.Empty;
            Duracion = 0;
            Contestadas = 0;
            NoContestadas = 0;
            Completado = false;
            Iniciado = false;
            TiempoTranscurrido = 0;
            FechaInicioTest = null;
            FechaFinTest = null;
            ConfiguracionTest = null;
            Evaluador= null;
            Paciente = null;
            Secciones =new List<SeccionesModel>();
            base.Id = 0;
            base.Creado = DateTime.Now;
            base.Eliminado = false;


        }
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


       
        public ConfiguracionTestModel? ConfiguracionTest { get; set; }

        [JsonIgnore]
        public EvaluadorModel? Evaluador { get; set; }

        [JsonIgnore]
        public PacienteModel? Paciente { get; set; }

       
        public ICollection<SeccionesModel>? Secciones { get; set; }
    }
}
