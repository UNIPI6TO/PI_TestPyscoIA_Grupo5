using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Ciudades")]
    public class CiudadModel:BaseModel
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        [JsonIgnore]
        public ProvinciaModel Provincia { get; set; }

    }
}
