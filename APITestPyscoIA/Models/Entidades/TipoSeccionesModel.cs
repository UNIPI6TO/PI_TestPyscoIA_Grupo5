using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("TipoSecciones")]
    public class TipoSeccionesModel:BaseModel
    {
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Duracion { get; set; } 

        [Required]
        public string Instrucciones { get; set; }

        public string Icono { get; set; }
        
        [Required]
        [ForeignKey("TipoTest")]
        public int IdTipoTest { get; set; }
        
        public TipoTestModel? TipoTest { get; set; }
        [JsonIgnore]
        public ICollection<ConfiguracionSeccionesModel>? ConfiguracionesSecciones { get; set; }
    }
}
