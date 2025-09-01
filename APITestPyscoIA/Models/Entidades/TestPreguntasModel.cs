using APITestPyscoIA.Models.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    public class TestPreguntasModel: BaseModel  
    {

        [Required] public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        [Precision(8, 4)]
        public decimal Peso { get; set; }

        [Required][ForeignKey("ConfiguracionPreguntas")] 
        public int IdConfiguracionBancoPreguntas { get; set; }
        
        [Required][ForeignKey("TestSecciones")] 
        public int IdTestSecciones { get; set; }
        
        public ConfiguracionPreguntasModel? ConfiguracionPreguntas { get; set; }
        
        public TestSeccionesModel? TestSecciones { get; set; }

    }
}
