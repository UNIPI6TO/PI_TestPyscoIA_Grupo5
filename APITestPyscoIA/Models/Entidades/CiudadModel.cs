using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Ciudades")]
    public class CiudadModel:BaseModel
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("ProvinciaModel")]
        public int IdProvincia { get; set; }

        public ProvinciaModel Provincia { get; set; }

    }
}
