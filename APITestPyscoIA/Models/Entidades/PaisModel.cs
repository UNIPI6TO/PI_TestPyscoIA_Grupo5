using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Paises")]   
    public class PaisModel: BaseModel
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public ICollection<ProvinciaModel> Provincias { get; set; }

    }
}
