using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<TipoSeccionesModel> Secciones { get; set; }

    }
}
