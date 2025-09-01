using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Provincias")]
    public class ProvinciaModel: BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("Pais")]
        public int IdPais { get; set; }

        
        public PaisModel? Pais { get; set; }
        [JsonIgnore]
        public ICollection<CiudadModel>? Ciudades { get; set; }

    }
}
