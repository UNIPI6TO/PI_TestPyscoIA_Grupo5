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
    public class OpcionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public OpcionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/Opciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpcionesModel>>> GetOpciones()
        {
            return await _context.Opciones.ToListAsync();
        }

        // GET: api/Opciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OpcionesModel>> GetOpcionesModel(int id)
        {
            var opcionesModel = await _context.Opciones.FindAsync(id);

            if (opcionesModel == null)
            {
                return NotFound();
            }

            return opcionesModel;
        }

        // PUT: api/Opciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOpcionesModel(int id, OpcionesModel opcionesModel)
        {
            if (id != opcionesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(opcionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpcionesModelExists(id))
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

        // POST: api/Opciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OpcionesModel>> PostOpcionesModel(OpcionesModel opcionesModel)
        {
            _context.Opciones.Add(opcionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOpcionesModel", new { id = opcionesModel.Id }, opcionesModel);
        }

        // DELETE: api/Opciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpcionesModel(int id)
        {
            var opcionesModel = await _context.Opciones.FindAsync(id);
            if (opcionesModel == null)
            {
                return NotFound();
            }

            _context.Opciones.Remove(opcionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpcionesModelExists(int id)
        {
            return _context.Opciones.Any(e => e.Id == id);
        }
    }
}
