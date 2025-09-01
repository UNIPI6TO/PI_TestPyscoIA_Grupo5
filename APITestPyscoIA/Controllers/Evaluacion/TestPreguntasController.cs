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
    [Route("api/[controller]")]
    [ApiController]
    public class TestPreguntasController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public TestPreguntasController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/TestPreguntas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestPreguntasModel>>> GetTestsPreguntas()
        {
            return await _context.TestsPreguntas.ToListAsync();
        }

        // GET: api/TestPreguntas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestPreguntasModel>> GetTestPreguntasModel(int id)
        {
            var testPreguntasModel = await _context.TestsPreguntas.FindAsync(id);

            if (testPreguntasModel == null)
            {
                return NotFound();
            }

            return testPreguntasModel;
        }

        // PUT: api/TestPreguntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestPreguntasModel(int id, TestPreguntasModel testPreguntasModel)
        {
            if (id != testPreguntasModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(testPreguntasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestPreguntasModelExists(id))
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

        // POST: api/TestPreguntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestPreguntasModel>> PostTestPreguntasModel(TestPreguntasModel testPreguntasModel)
        {
            _context.TestsPreguntas.Add(testPreguntasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestPreguntasModel", new { id = testPreguntasModel.Id }, testPreguntasModel);
        }

        // DELETE: api/TestPreguntas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestPreguntasModel(int id)
        {
            var testPreguntasModel = await _context.TestsPreguntas.FindAsync(id);
            if (testPreguntasModel == null)
            {
                return NotFound();
            }

            _context.TestsPreguntas.Remove(testPreguntasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestPreguntasModelExists(int id)
        {
            return _context.TestsPreguntas.Any(e => e.Id == id);
        }
    }
}
