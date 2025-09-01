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
    public class TestSeccionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public TestSeccionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/TestSecciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestSeccionesModel>>> GetTestsSecciones()
        {
            return await _context.TestsSecciones.ToListAsync();
        }

        // GET: api/TestSecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestSeccionesModel>> GetTestSeccionesModel(int id)
        {
            var testSeccionesModel = await _context.TestsSecciones.FindAsync(id);

            if (testSeccionesModel == null)
            {
                return NotFound();
            }

            return testSeccionesModel;
        }

        // PUT: api/TestSecciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestSeccionesModel(int id, TestSeccionesModel testSeccionesModel)
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
        public async Task<ActionResult<TestSeccionesModel>> PostTestSeccionesModel(TestSeccionesModel testSeccionesModel)
        {
            _context.TestsSecciones.Add(testSeccionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestSeccionesModel", new { id = testSeccionesModel.Id }, testSeccionesModel);
        }

        // DELETE: api/TestSecciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestSeccionesModel(int id)
        {
            var testSeccionesModel = await _context.TestsSecciones.FindAsync(id);
            if (testSeccionesModel == null)
            {
                return NotFound();
            }

            _context.TestsSecciones.Remove(testSeccionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestSeccionesModelExists(int id)
        {
            return _context.TestsSecciones.Any(e => e.Id == id);
        }
    }
}
