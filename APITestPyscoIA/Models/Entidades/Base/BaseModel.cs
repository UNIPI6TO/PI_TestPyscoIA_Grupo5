namespace APITestPyscoIA.Models.Entidades.Base
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime ? Actualizado { get; set; }
        [Display(Name = "Se puede eliminar ?")]
        public bool Eliminado { get; set; }
    }

}
