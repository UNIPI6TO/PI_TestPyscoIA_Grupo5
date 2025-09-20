using APITestPyscoIA.Models.Entidades.Base;
using System.Configuration;

namespace APITestPyscoIA.Models.ViewModel
{
    public class CrearUsuarioViewModel:BaseModel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        [RegexStringValidator("ADMIN|PACIENTE|EVALUADOR")]
        public string Rol { get; set; }
        public int? idEvaluador { get; set; }
        public int? idPaciente { get; set; }
    }
}
