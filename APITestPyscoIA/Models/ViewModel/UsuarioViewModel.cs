using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.ViewModel
{
    public class UsuarioViewModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
