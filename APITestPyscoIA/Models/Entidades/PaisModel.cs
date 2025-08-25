using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Paises")]   
    public class PaisModel: BaseModel
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [JsonIgnore]
        public ICollection<ProvinciaModel> Provincias { get; set; }

    }
}
