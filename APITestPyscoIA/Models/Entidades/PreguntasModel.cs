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
        public PreguntasModel()
        {
            Pregunta = string.Empty;
            Respuesta= string.Empty;
            Valor = 0;
            Orden = 0;
            IdConfiguracionPreguntas = 0;
            IdSecciones = 0;
            Secciones = null;
            ConfiguracionPreguntas = null;
            Opciones = new List<OpcionesModel>();
            base.Id = 0;
            base.Creado = DateTime.Now;
            base.Eliminado = false;
        }

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
        
        public ICollection<OpcionesModel> Opciones { get; set; }

        [Required]
        public int Orden { get; set; }

    }
}
