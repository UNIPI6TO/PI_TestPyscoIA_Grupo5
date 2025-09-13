using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITestPyscoIA.Data;
using APITestPyscoIA.Models.Entidades;
using APITestPyscoIA.Models.DTO;

namespace APITestPyscoIA.Controllers.Configuracion
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class ConfiguracionSeccionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ConfiguracionSeccionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/ConfiguracionSecciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionSeccionesModel>>> GetConfiguracionesSecciones()
        {
            return await _context.ConfiguracionesSecciones.ToListAsync();
        }

        // GET: api/config/ConfiguracionSecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionSeccionesModel>> GetConfiguracionSeccionesModel(int id)
        {
            var configuracionSeccionesModel = await _context.ConfiguracionesSecciones.FindAsync(id);

            if (configuracionSeccionesModel == null)
            {
                return NotFound();
            }

            return configuracionSeccionesModel;
        }

        // PUT: api/config/ConfiguracionSecciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracionSeccionesModel(int id, ConfiguracionSeccionesModel configuracionSeccionesModel)
        {
            if (id != configuracionSeccionesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(configuracionSeccionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracionSeccionesModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/config/ConfiguracionSecciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfiguracionSeccionesModel>> PostConfiguracionSeccionesModel(ConfiguracionesSecionDTO configuracionSeccionesDTO)
        {
            ConfiguracionSeccionesModel configuracionSeccionesModel = new ConfiguracionSeccionesModel();
            configuracionSeccionesModel.Seccion=configuracionSeccionesDTO.Seccion;
            configuracionSeccionesModel.FormulaAgregado = configuracionSeccionesDTO.FormulaAgregado;
            configuracionSeccionesModel.NumeroPreguntas = configuracionSeccionesDTO.NumeroPreguntas;
            configuracionSeccionesModel.Eliminado = false;
            configuracionSeccionesModel.Creado = DateTime.Now;
            configuracionSeccionesModel.IdConfiguracionesTest = configuracionSeccionesDTO.IdConfiguracionesTest;
            _context.ConfiguracionesSecciones.Add(configuracionSeccionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguracionSeccionesModel", new { id = configuracionSeccionesModel.Id }, configuracionSeccionesModel);
        }

        // DELETE: api/config/ConfiguracionSecciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracionSeccionesModel(int id)
        {
            var configuracionSeccionesModel = await _context.ConfiguracionesSecciones.FindAsync(id);
            if (configuracionSeccionesModel == null)
            {
                return NotFound();
            }
            configuracionSeccionesModel.Eliminado= true;
            configuracionSeccionesModel.Actualizado = DateTime.Now;
            _context.ConfiguracionesSecciones.Update(configuracionSeccionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfiguracionSeccionesModelExists(int id)
        {
            return _context.ConfiguracionesSecciones.Any(e => e.Id == id);
        }
    }
}
