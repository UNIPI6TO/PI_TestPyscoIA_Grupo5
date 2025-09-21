using APITestPyscoIA.ML_AI.Models;
using APITestPyscoIA.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITestPyscoIA.Controllers.Evaluacion
{
    [Route("[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        [HttpPost("Entrenar/Autoestima")]
        public ActionResult<MetricasEntrenamiento> EntrenarAutoestima(Muestras m)
        {
            Autoestima autoestima = new Autoestima();
            MetricasEntrenamiento metrica = autoestima.Entrenar(m.Cantidad);
            if (metrica != null)
            {
                return Ok(metrica);
            }
            return BadRequest();
        }

        [HttpGet("Predecir/Autoestima/{valor}")]
        public ActionResult<ResultModel> PredecirAutoestima(int valor)
        {
            ResultModel autoestima = new ResultModel();
            autoestima.Valor= valor;
            Autoestima autoestimaPredictor = new Autoestima();
            autoestima.Resultado = autoestimaPredictor.Prediccion(autoestima.Valor);
            return autoestima;
        }

        [HttpPost("Entrenar/Ansiedad")]
        public ActionResult<MetricasEntrenamiento> EntrenarAnsiedad(Muestras m)
        {
            Ansiedad ansiedad = new Ansiedad();
            MetricasEntrenamiento metrica = ansiedad.Entrenar(m.Cantidad);
            if (metrica != null)
            {
                return Ok(metrica);
            }
            return BadRequest();
        }

        [HttpGet("Predecir/Ansiedad/{valor}")]
        public ActionResult<ResultModel> PredecirAnsiedad(int valor)
        {
            ResultModel ansiedad = new ResultModel();
            ansiedad.Valor = valor;
            Ansiedad ansiedadPredictor = new Ansiedad();
            ansiedad.Resultado = ansiedadPredictor.Prediccion(ansiedad.Valor);
            return ansiedad;
        }

    }
    public class Muestras()
    {
        public int Cantidad { get; set; }

    }

    public class ResultModel()
    {
        public int Valor { get; set; }
        public string? Resultado { get; set; }
    }

}
