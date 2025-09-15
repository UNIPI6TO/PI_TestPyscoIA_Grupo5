using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITestPyscoIA.Data;
using APITestPyscoIA.Models.Entidades;

namespace APITestPyscoIA.Controllers.Evaluacion
{
    [Route("api/evaluacion/[controller]")]
    [ApiController]
    public class SeccionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public SeccionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/TestSecciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeccionesModel>>> GetTestsSecciones()
        {
            return await _context.Secciones.ToListAsync();
        }

        // GET: api/TestSecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeccionesModel>> GetTestSeccionesModel(int id)
        {
            var testSeccionesModel = await _context.Secciones.FindAsync(id);

            if (testSeccionesModel == null)
            {
                return NotFound();
            }

            return testSeccionesModel;
        }

        // PUT: api/TestSecciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestSeccionesModel(int id, SeccionesModel testSeccionesModel)
        {
            if (id != testSeccionesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(testSeccionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestSeccionesModelExists(id))
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

        // POST: api/TestSecciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeccionesModel>> PostTestSeccionesModel(SeccionesModel testSeccionesModel)
        {
            _context.Secciones.Add(testSeccionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestSeccionesModel", new { id = testSeccionesModel.Id }, testSeccionesModel);
        }

        // DELETE: api/TestSecciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestSeccionesModel(int id)
        {
            var testSeccionesModel = await _context.Secciones.FindAsync(id);
            if (testSeccionesModel == null)
            {
                return NotFound();
            }

            _context.Secciones.Remove(testSeccionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestSeccionesModelExists(int id)
        {
            return _context.Secciones.Any(e => e.Id == id);
        }
    }
}
