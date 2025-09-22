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
            autoestima.Valor = valor;
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


        [HttpPost("Entrenar/Depresion")]
        public ActionResult<MetricasEntrenamiento> EntrenarDepresion(Muestras m)
        {
            Depresion ansiedad = new Depresion();
            MetricasEntrenamiento metrica = ansiedad.Entrenar(m.Cantidad);
            if (metrica != null)
            {
                return Ok(metrica);
            }
            return BadRequest();
        }

        [HttpGet("Predecir/Depresion/{valor}")]
        public ActionResult<ResultModel> PredecirDepresion(int valor)
        {
            ResultModel depresion = new ResultModel();
            depresion.Valor = valor;
            Depresion depresionPredictor = new Depresion();
            depresion.Resultado = depresionPredictor.Prediccion(depresion.Valor);
            return depresion;
        }


        [HttpPost("Entrenar/Personalidad")]
        public ActionResult<MetricasEntrenamiento> EntrenarPersonalidad(Muestras m)
        {
            Personalidad personalidad = new Personalidad();
            MetricasEntrenamiento metrica = personalidad.Entrenar(m.Cantidad);
            if (metrica != null)
            {
                return Ok(metrica);
            }
            return BadRequest();
        }

        [HttpPost("Predecir/Personalidad")]
        public ActionResult<DatosPersonalidadModel> PredecirPersonalidad( DatosPersonalidadModel datos)
        {
            DatosPersonalidad personalidad = new DatosPersonalidad
            {
                Apertura = datos.Apertura,
                Responsabilidad = datos.Responsabilidad,
                Extroversion = datos.Extroversion,
                Amabilidad = datos.Amabilidad,
                Neuroticismo = datos.Neuroticismo   
            };
            Personalidad personalidadPredictor = new Personalidad();
            datos.Resultado = personalidadPredictor.Predecir(personalidad);
            return datos;
        }


    }
    public class Muestras()
    {
        public int Cantidad { get; set; }

    }

    public class DatosPersonalidadModel
    {
        public float Apertura { get; set; }
        public float Responsabilidad { get; set; }
        public float Extroversion { get; set; }
        public float Amabilidad { get; set; }
        public float Neuroticismo { get; set; }
        public string? Resultado { get; set; }
    }
    

    public class ResultModel()
    {
        public int Valor { get; set; }
        public string? Resultado { get; set; }
    }

}
