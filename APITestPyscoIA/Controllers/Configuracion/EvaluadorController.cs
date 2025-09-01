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
    public class EvaluadorController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public EvaluadorController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/Evaluador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluadorModel>>> GetEvaluadores()
        {
            return await _context.Evaluadores.ToListAsync();
        }

        // GET: api/config/Evaluador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluadorModel>> GetEvaluadorModel(int id)
        {
            var evaluadorModel = await _context.Evaluadores.FindAsync(id);

            if (evaluadorModel == null)
            {
                return NotFound();
            }

            return evaluadorModel;
        }

        // PUT: api/config/Evaluador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluadorModel(int id, EvaluadorModel evaluadorModel)
        {
            if (id != evaluadorModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluadorModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluadorModelExists(id))
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

        // POST: api/config/Evaluador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluadorModel>> PostEvaluadorModel(EvaluadorModel evaluadorModel)
        {
            _context.Evaluadores.Add(evaluadorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluadorModel", new { id = evaluadorModel.Id }, evaluadorModel);
        }

        // DELETE: api/config/Evaluador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluadorModel(int id)
        {
            var evaluadorModel = await _context.Evaluadores.FindAsync(id);
            if (evaluadorModel == null)
            {
                return NotFound();
            }

            _context.Evaluadores.Remove(evaluadorModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluadorModelExists(int id)
        {
            return _context.Evaluadores.Any(e => e.Id == id);
        }
    }
}
