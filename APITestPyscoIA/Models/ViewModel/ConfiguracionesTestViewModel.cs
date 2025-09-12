using APITestPyscoIA.Models.Entidades;
using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.ViewModel
{
    public class ConfiguracionesTestViewModel: BaseModel    
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public TipoTestModel? TipoTest { get; set; }
        public int NumeroSecciones { get; set; }
        public int NumeroPreguntas { get; set; }
        public ICollection<ConfiguracionSeccionesModel>? ConfiguracionesSecciones { get; set; }
        

    }
}
