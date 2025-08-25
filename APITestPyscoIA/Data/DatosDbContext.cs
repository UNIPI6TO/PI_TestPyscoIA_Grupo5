using APITestPyscoIA.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APITestPyscoIA.Data
{
    public class DatosDbContext : DbContext
    {
        public DatosDbContext(DbContextOptions<DatosDbContext> options) : base(options)
        {

        }
        public DbSet<CiudadModel> Ciudades { get; set; }
        public DbSet<ConfiguracionOpcionesModel> ConfiguracionesOpciones { get; set; }
        public DbSet<ConfiguracionPreguntasModel> ConfiguracionesPreguntas { get; set; }
        public DbSet<ConfiguracionSeccionesModel> ConfiguracionesSecciones { get; set; }
        public DbSet<ConfiguracionTestModel> ConfiguracionesTest { get; set; }
        public DbSet<EvaluadorModel> Evaluadores { get; set; }
        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<PaisModel> Paises { get; set; }
        public DbSet<ProvinciaModel> Provincias { get; set; }
        public DbSet<TestModel> Tests { get; set; }
        public DbSet<TestPreguntasModel> TestsPreguntas { get; set; }
        public DbSet<TestSeccionesModel> TestsSecciones { get; set; }
        public DbSet<TipoSeccionesModel> TiposSecciones { get; set; }
        public DbSet<TipoTestModel> TiposTest { get; set; }

    }
}
