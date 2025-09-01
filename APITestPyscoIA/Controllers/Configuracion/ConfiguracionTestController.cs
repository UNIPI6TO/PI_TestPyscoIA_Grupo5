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
    public class ConfiguracionTestController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ConfiguracionTestController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/ConfiguracionTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionTestModel>>> GetConfiguracionesTest()
        {
            return await _context.ConfiguracionesTest.ToListAsync();
        }

        // GET: api/config/ConfiguracionTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionTestModel>> GetConfiguracionTestModel(int id)
        {
            var configuracionTestModel = await _context.ConfiguracionesTest.FindAsync(id);

            if (configuracionTestModel == null)
            {
                return NotFound();
            }

            return configuracionTestModel;
        }

        // PUT: api/config/ConfiguracionTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracionTestModel(int id, ConfiguracionTestModel configuracionTestModel)
        {
            if (id != configuracionTestModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(configuracionTestModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracionTestModelExists(id))
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

        // POST: api/config/ConfiguracionTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfiguracionTestModel>> PostConfiguracionTestModel(ConfiguracionTestModel configuracionTestModel)
        {
            _context.ConfiguracionesTest.Add(configuracionTestModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguracionTestModel", new { id = configuracionTestModel.Id }, configuracionTestModel);
        }

        // DELETE: api/config/ConfiguracionTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracionTestModel(int id)
        {
            var configuracionTestModel = await _context.ConfiguracionesTest.FindAsync(id);
            if (configuracionTestModel == null)
            {
                return NotFound();
            }

            _context.ConfiguracionesTest.Remove(configuracionTestModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfiguracionTestModelExists(int id)
        {
            return _context.ConfiguracionesTest.Any(e => e.Id == id);
        }
    }
}
