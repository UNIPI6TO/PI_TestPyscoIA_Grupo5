using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITestPyscoIA.Data;
using APITestPyscoIA.Models.Entidades;
using APITestPyscoIA.Models.ViewModel;

namespace APITestPyscoIA.Controllers.Configuracion
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class ConfiguracionTestController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ConfiguracionTestController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/ConfiguracionTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracionTestModel>>> GetConfiguracionesTest()
        {
            return await _context.ConfiguracionesTest.Include(x=> x.TipoTest).ToListAsync();
        }

        [HttpGet("resumen")]
        public async Task<ActionResult<IEnumerable<ConfiguracionesTestViewModel>>> GetConfiguracionesResumen()
        {
            var resumen = await _context.ConfiguracionesTest
                .Where(a => a.Eliminado == false)
                .Select(a => new ConfiguracionesTestViewModel
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Duracion = a.Duracion,
                    Creado = a.Creado,
                    Actualizado = a.Actualizado,
                    Eliminado = a.Eliminado,
                    TipoTest = a.TipoTest,
                    NumeroSecciones = _context.ConfiguracionesSecciones.Count(s => s.IdConfiguracionesTest == a.Id),
                    NumeroPreguntas = (from b in _context.ConfiguracionesSecciones
                                       join c in _context.ConfiguracionesPreguntas on b.Id equals c.IdConfiguracionSecciones
                                       where b.IdConfiguracionesTest == a.Id
                                       select c).Count()
                })
                .ToListAsync();

            return resumen;
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<ConfiguracionesTestViewModel>> GetConfiguracionTestModelDataill(int id)
        {
            var resumen = await _context.ConfiguracionesTest
                .Where(a => a.Eliminado == false && a.Id==id)
                .Select(a => new ConfiguracionesTestViewModel
                {
                    Id = a.Id,
                    Nombre = a.Nombre,
                    Duracion = a.Duracion,
                    Creado = a.Creado,
                    Actualizado = a.Actualizado,
                    Eliminado = a.Eliminado,
                    TipoTest = a.TipoTest,
                    NumeroSecciones = _context.ConfiguracionesSecciones.Count(s => s.IdConfiguracionesTest == a.Id && a.Eliminado== false),
                    NumeroPreguntas = (from b in _context.ConfiguracionesSecciones
                                       join c in _context.ConfiguracionesPreguntas on b.Id equals c.IdConfiguracionSecciones 
                                       where b.IdConfiguracionesTest == a.Id && c.Eliminado == false && b.Eliminado== false
                                       select c).Count()
                })
                .FirstOrDefaultAsync();
            if (resumen == null || resumen.Id != id)
            {
                return NotFound(new { Mensaje="No se encontro la configuracion de evaluacion"});
            }

            resumen.ConfiguracionesSecciones = await _context.ConfiguracionesSecciones
                .Where(c=> c.IdConfiguracionesTest==resumen.Id && c.Eliminado== false)
                
                .ToListAsync();
            if(resumen.ConfiguracionesSecciones!= null )
                foreach (ConfiguracionSeccionesModel seccion in resumen.ConfiguracionesSecciones)
                {
                    seccion.BancoPreguntas =await _context.ConfiguracionesPreguntas
                        .Where(p => p.IdConfiguracionSecciones == seccion.Id && p.Eliminado == false)
                        .OrderByDescending(p=> p.Creado)
                        .ToListAsync();
                    if (seccion.BancoPreguntas!=null)
                        foreach(ConfiguracionPreguntasModel preguntas in seccion.BancoPreguntas)
                        {
                            preguntas.Opciones = await _context.ConfiguracionesOpciones
                                .Where(o => o.Eliminado == false && o.IdConfiguracionPreguntas==preguntas.Id)
                                .OrderBy(o=> o.Orden)
                                .ToListAsync();
                        } 

                }
            return Ok(resumen);
        }

        // GET: api/config/ConfiguracionTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracionTestModel>> GetConfiguracionTestModel(int id)
        {
            var configuracionTestModel = await _context.ConfiguracionesTest.FindAsync(id);

            if (configuracionTestModel == null)
            {
                return NotFound();
            }

            return configuracionTestModel;
        }

        // PUT: api/config/ConfiguracionTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracionTestModel(int id, ConfiguracionTestModel configuracionTestModel)
        {
            if (id != configuracionTestModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(configuracionTestModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfiguracionTestModelExists(id))
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

        // POST: api/config/ConfiguracionTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfiguracionTestModel>> PostConfiguracionTestModel(ConfiguracionTestModel configuracionTestModel)
        {
            _context.ConfiguracionesTest.Add(configuracionTestModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguracionTestModel", new { id = configuracionTestModel.Id }, configuracionTestModel);
        }


        // DELETE: api/config/ConfiguracionTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracionTestModel(int id)
        {
            var configuracionTestModel = await _context.ConfiguracionesTest.FindAsync(id);
            if (configuracionTestModel == null)
            {
                return NotFound();
            }
            configuracionTestModel.Eliminado = true;
            configuracionTestModel.Actualizado = DateTime.Now;
            _context.ConfiguracionesTest.Update(configuracionTestModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfiguracionTestModelExists(int id)
        {
            return _context.ConfiguracionesTest.Any(e => e.Id == id);
        }
    }
}
