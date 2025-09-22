using Microsoft.ML;
using Microsoft.ML.Data;
using NuGet.Protocol;
using System.Text.Json.Nodes;

namespace APITestPyscoIA.ML_AI.Models
{
    public class Ansiedad
    {
        private const string FILE_NAME = "ML_AI\\Entrenados\\modeloAnsiedadMLNET.zip";
        public MetricasEntrenamiento metricasEntrenamiento { get; set; }
        public Ansiedad()
        { 
            metricasEntrenamiento = new MetricasEntrenamiento();
        }
        public MetricasEntrenamiento Entrenar(int muestras)
        {

            var contexto = new MLContext();

            // Cargar los datos
            // Crear datos sintéticos aleatorios
            var rnd = new Random();
            var datosEjemplo = new List<DatosAnsiedad>();
            for (int i = 0; i < muestras; i++)
            {
                float valor = rnd.Next(0, 64);
                string resultado;
                if (valor >= 26)
                    resultado = "Ansiedad Grave";
                else if (valor >= 16)
                    resultado = "Ansiedad Moderada";
                else if (valor >= 8)
                    resultado = "Ansiedad Leve";
                else
                    resultado = "Ansiedad Mínima";
                datosEjemplo.Add(new DatosAnsiedad { Valor = valor, Resultado = resultado });
            }

            var datos = contexto.Data.LoadFromEnumerable(datosEjemplo);

            // Preparar el pipeline
            var pipeline = contexto.Transforms.Conversion.MapValueToKey("Label", nameof(DatosAnsiedad.Resultado))
            .Append(contexto.Transforms.Concatenate("Features", nameof(DatosAnsiedad.Valor)))

            .Append(contexto.MulticlassClassification.Trainers.LightGbm())
            .Append(contexto.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Console.WriteLine("Entrenando el modelo...");

            // Entrenar el modelo
            var modelo = pipeline.Fit(datos);
            // Evaluar el modelo
            var datosTest = datos;
            var predicciones = modelo.Transform(datosTest);
            var metricas = contexto.MulticlassClassification.Evaluate(predicciones);

            // Almacenar las métricas en variables
            metricasEntrenamiento.Precision = metricas.MicroAccuracy;
            metricasEntrenamiento.MacroAccuracy = metricas.MacroAccuracy;
            metricasEntrenamiento.LogLoss = metricas.LogLoss;
            metricasEntrenamiento.metrics = JsonNode.Parse(metricas.ToJson());
            Console.WriteLine(metricas.ToString());

            Console.WriteLine($"Precisión del modelo: {metricasEntrenamiento.Precision:P2}");
            Console.WriteLine($"Precisión macro: {metricasEntrenamiento.MacroAccuracy:P2}");
            Console.WriteLine($"LogLoss: {metricasEntrenamiento.LogLoss:F4}");
            Console.WriteLine("Modelo entrenado.");

            // Guardar el modelo
            contexto.Model.Save(modelo, datos.Schema, FILE_NAME);
            return metricasEntrenamiento;
        }
        public MetricasEntrenamiento Entrenar(List<DatosAnsiedad> datosEntrenamiento)
        {

            var contexto = new MLContext();

            // Cargar los datos
            var datos = contexto.Data.LoadFromEnumerable(datosEntrenamiento);

            // Preparar el pipeline
            var pipeline = contexto.Transforms.Conversion.MapValueToKey("Label", nameof(DatosAnsiedad.Resultado))
            .Append(contexto.Transforms.Concatenate("Features", nameof(DatosAnsiedad.Valor)))
            .Append(contexto.MulticlassClassification.Trainers.LightGbm())
            .Append(contexto.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            Console.WriteLine("Entrenando el modelo...");
            // Entrenar el modelo
            var modelo = pipeline.Fit(datos);
            // Evaluar el modelo
            var datosTest = datos;
            var predicciones = modelo.Transform(datosTest);
            var metricas = contexto.MulticlassClassification.Evaluate(predicciones);
            // Almacenar las métricas en variables
            metricasEntrenamiento.Precision = metricas.MicroAccuracy;
            metricasEntrenamiento.MacroAccuracy = metricas.MacroAccuracy;
            metricasEntrenamiento.LogLoss = metricas.LogLoss;
            Console.WriteLine($"Precisión del modelo: {metricasEntrenamiento.Precision:P2}");
            Console.WriteLine($"Precisión macro: {metricasEntrenamiento.MacroAccuracy:P2}");
            Console.WriteLine($"LogLoss: {metricasEntrenamiento.LogLoss:F4}");
            Console.WriteLine("Modelo entrenado.");
            // Guardar el modelo
            contexto.Model.Save(modelo, datos.Schema, FILE_NAME);
            return metricasEntrenamiento;
        }


        public string Prediccion(float Valor)
        {
            // Cargar el modelo entrenado
            var contexto = new MLContext();
            ITransformer modelo;
            using (var stream = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = contexto.Model.Load(stream, out var _);
            }

            var motor = contexto.Model.CreatePredictionEngine<DatosAnsiedad, PrediccionAnsiedad>(modelo);

            var nuevaEntrada = new DatosAnsiedad { Valor = Valor };
            var resultado = motor.Predict(nuevaEntrada);

            Console.WriteLine($"Predicción: {resultado.Prediccion}");
            return resultado.Prediccion != null ? resultado.Prediccion.ToString() : string.Empty;
        }


    }
    public class DatosAnsiedad
    {
        [LoadColumn(0)] public float Valor;
        [LoadColumn(1)] public string? Resultado;
    }
    public class PrediccionAnsiedad
    {
        [ColumnName("PredictedLabel")] public string? Prediccion;
    }
}
