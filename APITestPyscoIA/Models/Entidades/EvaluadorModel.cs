using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Evaluadores")]
    public class EvaluadorModel: BaseModel
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Cargo { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Especialidad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(10)]
        public  string Cedula { get; set; }

        [Required]
        [ForeignKey("Ciudad")]
        public int IdCiudad { get; set; }


        
        public CiudadModel? Ciudad { get; set; }
        public ICollection<TestModel>? Evaluaciones { get; set; }


    }
}
