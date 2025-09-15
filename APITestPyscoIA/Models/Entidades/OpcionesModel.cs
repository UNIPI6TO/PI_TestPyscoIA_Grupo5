using APITestPyscoIA.Models.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Opciones")]
    public class OpcionesModel: BaseModel
    {
        public OpcionesModel()
        {
            Orden = 0;
            Opcion = string.Empty;
            Peso = 0;
            seleccionado = false;
            IdPreguntas = 0;
            Preguntas = null;
            base.Id = 0;
            base.Creado = DateTime.Now;
            base.Eliminado = false;
        }
        [Required] public int Orden { get; set; }
        [Required] public string Opcion { get; set; }

        [Precision(8, 4)][Required] public decimal Peso { get; set; }

        public bool? seleccionado { get; set; } = false;

        [ForeignKey("Preguntas")]
        public int IdPreguntas { get; set; }


        [JsonIgnore]
        public PreguntasModel ? Preguntas { get; set; }

    }
}
