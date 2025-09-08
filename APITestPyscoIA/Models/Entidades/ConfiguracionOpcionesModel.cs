﻿using APITestPyscoIA.Models.Entidades.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITestPyscoIA.Models.Entidades
{
    [Table("ConfiguracionesOpciones")]
    public class ConfiguracionOpcionesModel: BaseModel
    {


        [Required] public int Orden { get; set; }
        [Required] public string Opcion { get; set; }
        
        [Precision(8, 4)][Required] public decimal Peso { get; set; }

        [ForeignKey("ConfiguracionPreguntas")]
        public int IdConfiguracionPreguntas { get; set; }


        public ConfiguracionPreguntasModel ? ConfiguracionPreguntas { get; set; }
    }
}
