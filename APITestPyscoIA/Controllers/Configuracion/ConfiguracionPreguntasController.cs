using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITestPyscoIA.Data;
using APITestPyscoIA.Models.Entidades;

namespace APITestPyscoIA.Controllers.Configuracion
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class ConfiguracionPreguntasController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ConfiguracionPreguntasController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/ConfiguracionPreguntas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionPreguntasModel>>> GetConfiguracionesPreguntas()
        {
            return await _context.ConfiguracionesPreguntas.ToListAsync();
        }

        // GET: api/config/ConfiguracionPreguntas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionPreguntasModel>> GetConfiguracionPreguntasModel(int id)
        {
            var configuracionPreguntasModel = await _context.ConfiguracionesPreguntas.FindAsync(id);

            if (configuracionPreguntasModel == null)
            {
                return NotFound();
            }

            return configuracionPreguntasModel;
        }

        // PUT: api/config/ConfiguracionPreguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracionPreguntasModel(int id, ConfiguracionPreguntasModel configuracionPreguntasModel)
        {
            if (id != configuracionPreguntasModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(configuracionPreguntasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracionPreguntasModelExists(id))
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

        // POST: api/config/ConfiguracionPreguntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfiguracionPreguntasModel>> PostConfiguracionPreguntasModel(ConfiguracionPreguntasModel configuracionPreguntasModel)
        {
            _context.ConfiguracionesPreguntas.Add(configuracionPreguntasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguracionPreguntasModel", new { id = configuracionPreguntasModel.Id }, configuracionPreguntasModel);
        }

        // DELETE: api/config/ConfiguracionPreguntas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracionPreguntasModel(int id)
        {
            var configuracionPreguntasModel = await _context.ConfiguracionesPreguntas.FindAsync(id);
            if (configuracionPreguntasModel == null)
            {
                return NotFound();
            }

            _context.ConfiguracionesPreguntas.Remove(configuracionPreguntasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfiguracionPreguntasModelExists(int id)
        {
            return _context.ConfiguracionesPreguntas.Any(e => e.Id == id);
        }
    }
}
