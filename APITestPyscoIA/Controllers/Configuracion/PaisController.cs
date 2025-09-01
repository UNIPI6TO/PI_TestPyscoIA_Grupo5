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
    public class PaisController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public PaisController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/Pais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisModel>>> GetPaises()
        {
            return await _context.Paises.ToListAsync();
        }

        // GET: api/config/Pais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaisModel>> GetPaisModel(int id)
        {
            var paisModel = await _context.Paises.FindAsync(id);

            if (paisModel == null)
            {
                return NotFound();
            }

            return paisModel;
        }

        // PUT: api/config/Pais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaisModel(int id, PaisModel paisModel)
        {
            if (id != paisModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(paisModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisModelExists(id))
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

        // POST: api/config/Pais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaisModel>> PostPaisModel(PaisModel paisModel)
        {
            _context.Paises.Add(paisModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaisModel", new { id = paisModel.Id }, paisModel);
        }

        // DELETE: api/config/Pais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaisModel(int id)
        {
            var paisModel = await _context.Paises.FindAsync(id);
            if (paisModel == null)
            {
                return NotFound();
            }

            _context.Paises.Remove(paisModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaisModelExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
