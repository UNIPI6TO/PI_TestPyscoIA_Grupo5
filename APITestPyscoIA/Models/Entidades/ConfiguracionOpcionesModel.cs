using APITestPyscoIA.Models.Entidades.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITestPyscoIA.Models.Entidades
{
    public class ConfiguracionOpcionesModel: BaseModel
    {


        [Required] public int Orden { get; set; }
        [Required] public string Opcion { get; set; }
        [Required] public decimal Peso { get; set; }

        [Required][ForeignKey("ConfiguracionBancoPreguntas")] public int IdConfiguracionBancoPreguntas { get; set; }
        public ConfiguracionPreguntasModel ConfiguracionBancoPreguntas { get; set; }

    }
}
