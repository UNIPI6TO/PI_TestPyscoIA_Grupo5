using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace APITestPyscoIA.ML_AI.Models
{
    public class MetricasEntrenamiento
    {
        public MetricasEntrenamiento()
        {
            Precision = -1.0;
            LogLoss = -1.0;
            MacroAccuracy = -1.0;
            MicroAccuracy = -1.0;
        }
        // Métricas para clasificación
        public double Precision { get; set; }
        public double Accuracy { get; set; }
        public double F1Score { get; set; }
        public double LogLoss { get; set; }
        public double LogLossReduction { get; set; }
        public double MacroAccuracy { get; set; }
        public double MicroAccuracy { get; set; }

        public JsonNode metrics { get; set; }
    }
}