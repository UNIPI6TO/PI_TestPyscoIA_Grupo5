using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("Pacientes")]
    public class PacienteModel : BaseModel
    {

        [Required]
        [StringLength(10)]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [ForeignKey("Ciudad")]
        public int IdCiudad { get; set; }

        public CiudadModel? Ciudad { get; set; }

    }

    public class PacienteDTO
    {
        public int? Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string? Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int IdCiudad { get; set; }

    }
}
