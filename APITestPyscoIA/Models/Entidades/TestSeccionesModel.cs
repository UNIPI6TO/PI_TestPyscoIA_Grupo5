using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("TestSecciones")] 
    public class TestSeccionesModel:BaseModel
    {
        public float? Score { get; set; }
        public string? Resultado { get; set; }
        public DateTime? FechaInicioTest { get; set; }
        public DateTime? FechaFinTest { get; set; }

        [Required]
        [ForeignKey("Test")] 
        public int IdTest { get; set; }
        [Required]
        [ForeignKey("ConfiguracionSecciones")] 
        public int IdConfiguracionSecciones { get; set; }

        
        public TestModel? Test { get; set; }
        
        public ConfiguracionSeccionesModel? ConfiguracionSecciones { get; set; }
        [JsonIgnore]
        public ICollection<TestPreguntasModel>? Preguntas { get; set; }

    }
}
