using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionesTest")]
    public class ConfiguracionTestModel : BaseModel
    {

        [Required]
        public string Nombre { get; set; }
        
        [Required]
        [ForeignKey("TipoTest")]
        public int IdTipoTest { get; set; }

        [Required]
        public int Duracion { get; set; }

        public TipoTestModel? TipoTest { get; set; }
        
        [JsonIgnore]
        public ICollection<ConfiguracionSeccionesModel>? ConfiguracionesSecciones { get; set; }
    }
}
