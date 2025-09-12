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
    [Route("api/evaluacion/[controller]")]
    [ApiController]
    public class TipoTestController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public TipoTestController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTestModel>>> GetTiposTest()
        {
            return await _context.TiposTest.Where(x=> x.Eliminado==false).ToListAsync();
        }

        // GET: api/TipoTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTestModel>> GetTipoTestModel(int id)
        {
            var tipoTestModel = await _context.TiposTest.FindAsync(id);

            if (tipoTestModel == null)
            {
                return NotFound();
            }

            return tipoTestModel;
        }

        // PUT: api/TipoTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTestModel(int id, TipoTestModel tipoTestModel)
        {
            if (id != tipoTestModel.Id)
            {
                return BadRequest();
            }
            tipoTestModel.Actualizado = DateTime.Now;
            _context.Entry(tipoTestModel).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTestModelExists(id))
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

        // POST: api/TipoTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoTestModel>> PostTipoTestModel(TipoTestModel tipoTestModel)
        {
            _context.TiposTest.Add(tipoTestModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTestModel", new { id = tipoTestModel.Id }, tipoTestModel);
        }

        // DELETE: api/TipoTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTestModel(int id)
        {
            var tipoTestModel = await _context.TiposTest.FindAsync(id);
            if (tipoTestModel == null)
            {
                return NotFound();
            }

            tipoTestModel.Eliminado = true;
            tipoTestModel.Actualizado = DateTime.Now;
            _context.TiposTest.Update(tipoTestModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoTestModelExists(int id)
        {
            return _context.TiposTest.Any(e => e.Id == id);
        }
    }
}
