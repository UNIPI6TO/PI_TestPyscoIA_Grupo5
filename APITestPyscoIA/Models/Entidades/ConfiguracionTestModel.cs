using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionesTest")]
    public class ConfiguracionTestModel : BaseModel
    {

        [Required]
        public string Nombre { get; set; }
        [Required]
        public int TiempoExpiracion { get; set; }

        [Required]
        [ForeignKey("TipoTestModel")]
        public int IdTipoTest { get; set; }
        [Required]
        [ForeignKey("EvaluadorModel")]
        public int IdEvaluador { get; set; }

        public TipoTestModel TipoTest { get; set; }
        public EvaluadorModel Evaluador { get; set; }

        public ICollection<ConfiguracionSeccionesModel> ConfiguracionesSecciones { get; set; }
    }
}
