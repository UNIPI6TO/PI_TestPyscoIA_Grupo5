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
    public class ProvinciaController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ProvinciaController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/Provincia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvinciaModel>>> GetProvincias()
        {
            return await _context.Provincias.ToListAsync();
        }

        // GET: api/config/Provincia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvinciaModel>> GetProvinciaModel(int id)
        {
            var provinciaModel = await _context.Provincias.FindAsync(id);

            if (provinciaModel == null)
            {
                return NotFound();
            }

            return provinciaModel;
        }

        // PUT: api/config/Provincia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvinciaModel(int id, ProvinciaModel provinciaModel)
        {
            if (id != provinciaModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(provinciaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciaModelExists(id))
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

        // POST: api/config/Provincia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProvinciaModel>> PostProvinciaModel(ProvinciaModel provinciaModel)
        {
            _context.Provincias.Add(provinciaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvinciaModel", new { id = provinciaModel.Id }, provinciaModel);
        }

        // DELETE: api/config/Provincia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvinciaModel(int id)
        {
            var provinciaModel = await _context.Provincias.FindAsync(id);
            if (provinciaModel == null)
            {
                return NotFound();
            }

            _context.Provincias.Remove(provinciaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinciaModelExists(int id)
        {
            return _context.Provincias.Any(e => e.Id == id);
        }
    }
}
