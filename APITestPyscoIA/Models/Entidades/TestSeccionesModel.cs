using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    public class TestSeccionesModel:BaseModel
    {
        public float Score { get; set; }
        public string Resultado { get; set; }
        public DateTime? FechaInicioTest { get; set; }
        public DateTime? FechaFinTest { get; set; }
        public int Duracion { get; set; }
        public int Contestadas { get; set; }
        public int NoContestadas { get; set; }
        public int CantidadPreguntas { get; set; }
        public bool Completado { get; set; }
        public bool Iniciado { get; set; }

        [Required]
        [ForeignKey("TestModel")] 
        public int IdTest { get; set; }
        [Required]
        [ForeignKey("ConfiguracionSeccionesModel")] 
        public int IdConfiguracionSecciones { get; set; }

        public TestModel Test { get; set; }
        public ConfiguracionSeccionesModel ConfiguracionSecciones { get; set; }

        public ICollection<TestPreguntasModel> Preguntas { get; set; }

    }
}
