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
    public class ConfiguracionOpcionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ConfiguracionOpcionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/ConfiguracionOpciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionOpcionesModel>>> GetConfiguracionesOpciones()
        {
            return await _context.ConfiguracionesOpciones.ToListAsync();
        }

        // GET: api/config/ConfiguracionOpciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionOpcionesModel>> GetConfiguracionOpcionesModel(int id)
        {
            var configuracionOpcionesModel = await _context.ConfiguracionesOpciones.FindAsync(id);

            if (configuracionOpcionesModel == null)
            {
                return NotFound();
            }

            return configuracionOpcionesModel;
        }

        // PUT: api/config/ConfiguracionOpciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracionOpcionesModel(int id, ConfiguracionOpcionesModel configuracionOpcionesModel)
        {
            if (id != configuracionOpcionesModel.Id)
            {
                return BadRequest();
            }
            configuracionOpcionesModel.Actualizado = DateTime.Now;
            
            _context.Entry(configuracionOpcionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracionOpcionesModelExists(id))
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

        // POST: api/config/ConfiguracionOpciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfiguracionOpcionesModel>> PostConfiguracionOpcionesModel(ConfiguracionOpcionesModel configuracionOpcionesModel)
        {
            configuracionOpcionesModel.Creado = DateTime.Now;
            configuracionOpcionesModel.Eliminado = false;
            _context.ConfiguracionesOpciones.Add(configuracionOpcionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguracionOpcionesModel", new { id = configuracionOpcionesModel.Id }, configuracionOpcionesModel);
        }

        // DELETE: api/config/ConfiguracionOpciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracionOpcionesModel(int id)
        {
            var configuracionOpcionesModel = await _context.ConfiguracionesOpciones.FindAsync(id);
            if (configuracionOpcionesModel == null)
            {
                return NotFound();
            }
            configuracionOpcionesModel.Eliminado = true;
            configuracionOpcionesModel.Actualizado = DateTime.Now;
            _context.ConfiguracionesOpciones.Update(configuracionOpcionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfiguracionOpcionesModelExists(int id)
        {
            return _context.ConfiguracionesOpciones.Any(e => e.Id == id);
        }
    }
}
