using APITestPyscoIA.ML_AI.Models;
using APITestPyscoIA.Models.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("Predecir/Autoestima")]
        public ActionResult<ResultModel> PredecirAutoestima(DatosEntradaGenericoAI datos)
        {
            if (datos == null)
                return BadRequest();
            DatoEntradaGenericoAI? dato = datos.Valores
                .Find(x => x.Key == "Autoestima");
            if (dato == null)
                return NotFound();
            Autoestima autoestimaPredictor = new Autoestima();
            ResultModel resultModel = new ResultModel();
            resultModel.Valor = dato.Valor;

            resultModel.Resultado = autoestimaPredictor.Prediccion(dato.Valor);

            return resultModel;
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

        [HttpPost("Predecir/Ansiedad")]
        public ActionResult<ResultModel> PredecirAnsiedad(DatosEntradaGenericoAI datos)
        {
            if (datos == null)
                return BadRequest();
            DatoEntradaGenericoAI? dato = datos.Valores
                .Find(x => x.Key == "Ansiedad");
            if (dato == null)
                return NotFound();
            Ansiedad depresionPredictor = new Ansiedad();
            ResultModel resultModel = new ResultModel();
            resultModel.Valor = dato.Valor;

            resultModel.Resultado = depresionPredictor.Prediccion(dato.Valor);

            return resultModel;
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

        [HttpPost("Predecir/Depresion")]
        public ActionResult<ResultModel> PredecirDepresion(DatosEntradaGenericoAI datos)
        {
            if (datos==null)
                return BadRequest();
            DatoEntradaGenericoAI? dato =datos.Valores
                .Find(x => x.Key == "Depresión");
            if (dato==null)
                return NotFound();
            Depresion depresionPredictor = new Depresion();
            ResultModel resultModel = new ResultModel();
            resultModel.Valor = dato.Valor;

            resultModel.Resultado = depresionPredictor.Prediccion(dato.Valor);

            return resultModel;
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
        public ActionResult<ResultModel> PredecirPersonalidad(DatosEntradaGenericoAI datos)
        {
            DatosPersonalidad datosPersonalidad = new DatosPersonalidad();
            if (datos == null)
                return BadRequest();
            
            DatoEntradaGenericoAI? Apertura = datos.Valores
                .Find(x => x.Key == "Apertura");

            if (Apertura == null)
                return NotFound();

            DatoEntradaGenericoAI? Responsabilidad = datos.Valores
                .Find(x => x.Key == "Responsabilidad");

            if (Responsabilidad == null)
                return NotFound();

            DatoEntradaGenericoAI? Extroversion = datos.Valores
                .Find(x => x.Key == "Extroversión");

            if (Extroversion == null)
                return NotFound();

            DatoEntradaGenericoAI? Amabilidad = datos.Valores
                .Find(x => x.Key == "Amabilidad");

            if (Amabilidad == null)
                return NotFound();

            DatoEntradaGenericoAI? Neuroticismo = datos.Valores
                .Find(x => x.Key == "Neuroticismo");

            if (Neuroticismo == null)
                return NotFound();

            datosPersonalidad.Apertura = Apertura.Valor;
            datosPersonalidad.Responsabilidad = Responsabilidad.Valor;
            datosPersonalidad.Extroversion = Extroversion.Valor;
            datosPersonalidad.Amabilidad = Amabilidad.Valor;
            datosPersonalidad.Neuroticismo = Neuroticismo.Valor;

            Personalidad personalidadPredictor = new Personalidad();
            ResultModel resultModel = new ResultModel();
            

            resultModel.Resultado= personalidadPredictor.Prediccion(datosPersonalidad);

            return resultModel;
        }


    }
    public class Muestras()
    {
        public int Cantidad { get; set; }

    }

    public class DatosEntradaGenericoAI()
    {
        public  List<DatoEntradaGenericoAI> Valores { get; set ; }

    }

    public class DatoEntradaGenericoAI()
    {
        public string Key { get; set; }
        public float Valor { get; set; }
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
        public float Valor { get; set; }
        public string? Resultado { get; set; }
    }
    

}
