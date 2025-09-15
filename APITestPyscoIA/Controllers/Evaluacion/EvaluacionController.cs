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
    public class EvaluacionController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public EvaluacionController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/EvaluacionModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionesModel>>> GetEvaluaciones()
        {
            return await _context.Tests.ToListAsync();
        }

        // GET: api/EvaluacionModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionesModel>> GetEvaluaciones(int id)
        {
            var evaluacionesModel = await _context.Tests.FindAsync(id);

            if (evaluacionesModel == null)
            {
                return NotFound();
            }

            return evaluacionesModel;
        }

        [HttpGet("paciente/{idPaciente}")]
        public async Task<ActionResult<IEnumerable<EvaluacionesModel>>> GetEvaluacionesByPaciente(int idPaciente)
        {
            var evaluacionesModel = await _context.Tests.
                Where(e=> e.IdPaciente==idPaciente && e.Eliminado== false)
                .ToListAsync();

            if (evaluacionesModel == null)
            {
                return NotFound();
            }

            return evaluacionesModel;
        }

        // PUT: api/EvaluacionModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluaciones(int id, EvaluacionesModel evaluacionesModel)
        {
            if (id != evaluacionesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluacionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluacionesExists(id))
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

        // POST: api/EvaluacionModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluacionesModel>> PostEvaluaciones(EvaluacionesModel evaluacionesModel)
        {
            _context.Tests.Add(evaluacionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluacionesModel", new { id = evaluacionesModel.Id }, evaluacionesModel);
        }

        // DELETE: api/EvaluacionModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluaciones(int id)
        {
            var evaluacionesModel = await _context.Tests.FindAsync(id);
            if (evaluacionesModel == null)
            {
                return NotFound();
            }
            evaluacionesModel.Eliminado = true;
            evaluacionesModel.Actualizado = DateTime.Now;
            _context.Tests.Update(evaluacionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluacionesExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
    }
}
