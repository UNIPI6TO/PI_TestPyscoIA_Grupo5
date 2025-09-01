using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Test")]
    public class TestModel : BaseModel  
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

        public int? Duracion { get; set; }
        public int? Contestadas { get; set; }
        public int? NoContestadas { get; set; }
        public bool? Completado { get; set; }
        public bool? Iniciado { get; set; }


        public ConfiguracionTestModel? ConfiguracionTest { get; set; }
        
        public EvaluadorModel? Evaluador { get; set; }
        
        public PacienteModel? Paciente { get; set; }
        [JsonIgnore]
        public ICollection<TestSeccionesModel>? Secciones { get; set; }
    }
}
