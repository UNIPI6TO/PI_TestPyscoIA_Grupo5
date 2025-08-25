using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("TipoSecciones")]
    public class TipoSeccionesModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Duracion { get; set; } 
        public int Orden { get; set; }

        public string Icono { get; set; }
        [Required]
        [ForeignKey("TipoTestModel")]
        public int IdTipoTest { get; set; }

        public TipoTestModel TipoTest { get; set; }

        public ICollection<ConfiguracionSeccionesModel> ConfiguracionesSecciones { get; set; }


    }
}
