using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("TipoTest")]
    public class TipoTestModel: BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }
        [Required]
        public string Instrucciones { get; set; }
        [JsonIgnore]
        public ICollection<TipoSeccionesModel>? Secciones { get; set; }

    }
}
