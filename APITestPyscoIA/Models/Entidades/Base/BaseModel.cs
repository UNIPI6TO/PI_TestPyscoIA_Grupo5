namespace APITestPyscoIA.Models.Entidades.Base
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class BaseModel
    {
        [Key]
        [Required(ErrorMessage ="Campo Requerido")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime Creado { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ? Actualizado { get; set; }
        [Display(Name = "Está eliminado ?")]
        public bool? Eliminado { get; set; }
        [JsonIgnore]
        public bool? Sincronizado { get; set; }

    }

}
