using Microsoft.ML;
using Microsoft.ML.Data;
using NuGet.Protocol;
using System.Text.Json.Nodes;

namespace APITestPyscoIA.ML_AI.Models
{
    public class Personalidad
    {
        private const string FILE_NAME = "ML_AI\\Entrenados\\modeloPersonalidadMLNET.zip";
        public MetricasEntrenamiento metricasEntrenamiento { get; set; }
        public Personalidad()
        {
            metricasEntrenamiento = new MetricasEntrenamiento();
        }
        public MetricasEntrenamiento Entrenar(int muestras)
        {

            var contexto = new MLContext();

            // Cargar los datos
            // Crear datos sintéticos aleatorios
            var rnd = new Random();
            var datosSinteticos = new List<DatosPersonalidad>();
            for (int i = 0; i < muestras; i++)
            {

                DatosPersonalidad dato = new DatosPersonalidad();

                dato.Apertura = rnd.Next(10, 50) / 10f;
                dato.Responsabilidad = rnd.Next(10, 50) / 10f;
                dato.Extroversion = rnd.Next(10, 50) / 10f;
                dato.Amabilidad = rnd.Next(10, 50) / 10f;
                dato.Neuroticismo = rnd.Next(10, 50) / 10f;

                // Obtener el mayor valor y determinar cuál fue
                var valores = new Dictionary<string, float>
                {
                    { "Apertura", dato.Apertura },
                    { "Responsabilidad", dato.Responsabilidad },
                    { "Extroversión", dato.Extroversion },
                    { "Amabilidad", dato.Amabilidad },
                    { "Neuroticismo", dato.Neuroticismo }
                };
                var max = valores.Aggregate((l, r) => l.Value > r.Value ? l : r);
                dato.Resultado = max.Key;
                var valor = max.Value;

                // Almacenar los resultados
                datosSinteticos.Add(dato);
            }

            var datos = contexto.Data.LoadFromEnumerable(datosSinteticos);
            // Preparar el pipeline
            var pipeline = contexto.Transforms.Conversion.MapValueToKey("Label", nameof(DatosPersonalidad.Resultado))
                .Append(contexto.Transforms.Concatenate("Features",
                    nameof(DatosPersonalidad.Apertura),
                    nameof(DatosPersonalidad.Responsabilidad),
                    nameof(DatosPersonalidad.Extroversion),
                    nameof(DatosPersonalidad.Amabilidad),
                    nameof(DatosPersonalidad.Neuroticismo)))
                .Append(contexto.MulticlassClassification.Trainers.LightGbm())
                .Append(contexto.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


            var modelo = pipeline.Fit(datos);
            Console.WriteLine("Entrenando el modelo...");

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
        public MetricasEntrenamiento Entrenar(List<DatosPersonalidad> datosEntrenamiento)
        {

            var contexto = new MLContext();

            // Cargar los datos
            var datos = contexto.Data.LoadFromEnumerable(datosEntrenamiento);
            // Preparar el pipeline
            var pipeline = contexto.Transforms.Conversion.MapValueToKey("Label", nameof(DatosPersonalidad.Resultado))
                .Append(contexto.Transforms.Concatenate("Features",
                    nameof(DatosPersonalidad.Apertura),
                    nameof(DatosPersonalidad.Responsabilidad),
                    nameof(DatosPersonalidad.Extroversion),
                    nameof(DatosPersonalidad.Amabilidad),
                    nameof(DatosPersonalidad.Neuroticismo)))
                .Append(contexto.MulticlassClassification.Trainers.LightGbm())
                .Append(contexto.Transforms.Conversion.MapKeyToValue("PredictedLabel"));


            var modelo = pipeline.Fit(datos);
            Console.WriteLine("Entrenando el modelo...");

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
        public string Predecir(DatosPersonalidad datosEntrada)
        {
            var contexto = new MLContext();
            ITransformer modelo;
            DataViewSchema esquema;
            using (var archivo = System.IO.File.OpenRead(FILE_NAME))
            {
                modelo = contexto.Model.Load(archivo, out esquema);
            }
            var prediccionEngine = contexto.Model.CreatePredictionEngine<DatosPersonalidad, PrediccionPersonalidad>(modelo);
            var resultado = prediccionEngine.Predict(datosEntrada);
            Console.WriteLine($"Predicción: {resultado.Prediccion}");
            return resultado.Prediccion != null ? resultado.Prediccion.ToString() : string.Empty;

        }
    }
}
public class DatosPersonalidad
    {
        [LoadColumn(0)] public float Apertura;
        [LoadColumn(1)] public float Responsabilidad;
        [LoadColumn(2)] public float Extroversion;
        [LoadColumn(3)] public float Amabilidad;
        [LoadColumn(4)] public float Neuroticismo;
        [LoadColumn(5)] public string? Resultado;
    }
public class PrediccionPersonalidad
{
    [ColumnName("PredictedLabel")] public string? Prediccion;
}

