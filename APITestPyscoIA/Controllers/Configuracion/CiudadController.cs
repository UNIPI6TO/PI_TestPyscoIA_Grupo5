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
    public class CiudadController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public CiudadController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/Ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CiudadModel>>> GetCiudades()
        {
            return await _context.Ciudades.ToListAsync();
        }

        // GET: api/config/Ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CiudadModel>> GetCiudadModel(int id)
        {
            var ciudadModel = await _context.Ciudades.FindAsync(id);

            if (ciudadModel == null)
            {
                return NotFound();
            }

            return ciudadModel;
        }

        // PUT: api/config/Ciudad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudadModel(int id, CiudadModel ciudadModel)
        {
            if (id != ciudadModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(ciudadModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadModelExists(id))
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

        // POST: api/config/Ciudad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CiudadModel>> PostCiudadModel(CiudadModel ciudadModel)
        {
            _context.Ciudades.Add(ciudadModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudadModel", new { id = ciudadModel.Id }, ciudadModel);
        }

        // DELETE: api/config/Ciudad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudadModel(int id)
        {
            var ciudadModel = await _context.Ciudades.FindAsync(id);
            if (ciudadModel == null)
            {
                return NotFound();
            }

            _context.Ciudades.Remove(ciudadModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CiudadModelExists(int id)
        {
            return _context.Ciudades.Any(e => e.Id == id);
        }
    }
}
