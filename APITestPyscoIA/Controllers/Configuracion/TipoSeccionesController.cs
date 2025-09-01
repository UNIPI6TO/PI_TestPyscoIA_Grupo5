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
    public class TipoSeccionesController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public TipoSeccionesController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/TipoSecciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSeccionesModel>>> GetTiposSecciones()
        {
            return await _context.TiposSecciones.ToListAsync();
        }

        // GET: api/config/TipoSecciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSeccionesModel>> GetTipoSeccionesModel(int id)
        {
            var tipoSeccionesModel = await _context.TiposSecciones.FindAsync(id);

            if (tipoSeccionesModel == null)
            {
                return NotFound();
            }

            return tipoSeccionesModel;
        }

        // PUT: api/config/TipoSecciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoSeccionesModel(int id, TipoSeccionesModel tipoSeccionesModel)
        {
            if (id != tipoSeccionesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoSeccionesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoSeccionesModelExists(id))
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

        // POST: api/config/TipoSecciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoSeccionesModel>> PostTipoSeccionesModel(TipoSeccionesModel tipoSeccionesModel)
        {
            _context.TiposSecciones.Add(tipoSeccionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoSeccionesModel", new { id = tipoSeccionesModel.Id }, tipoSeccionesModel);
        }

        // DELETE: api/config/TipoSecciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoSeccionesModel(int id)
        {
            var tipoSeccionesModel = await _context.TiposSecciones.FindAsync(id);
            if (tipoSeccionesModel == null)
            {
                return NotFound();
            }

            _context.TiposSecciones.Remove(tipoSeccionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoSeccionesModelExists(int id)
        {
            return _context.TiposSecciones.Any(e => e.Id == id);
        }
    }
}
