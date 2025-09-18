using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text.Json.Serialization;


namespace APITestPyscoIA.Models.Entidades
{
    [Table("Usuario")]
    public class UsuarioModel : BaseModel
    {
        [Required]
        public string Usuario { get; set; }
        
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        [RegexStringValidator("ADMIN|PACIENTE|EVALUADOR")]
        public string Rol { get; set; }
        public int? idEvaluador { get; set; }
        public int? idPaciente { get; set; }

    }
}
