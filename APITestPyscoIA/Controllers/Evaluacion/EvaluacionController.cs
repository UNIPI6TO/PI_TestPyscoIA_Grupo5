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
using System.Xml;

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
            return await _context.Evaluaciones.ToListAsync();
        }

        // GET: api/EvaluacionModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionesModel>> GetEvaluaciones(int id)
        {
            
            EvaluacionesModel? evaluacionesModel = await _context.Evaluaciones.FindAsync(id);
            

            if (evaluacionesModel == null)
            {
                return NotFound();
            }
            evaluacionesModel.ConfiguracionTest = await _context.ConfiguracionesTest.Include(t => t.TipoTest)
                   .FirstOrDefaultAsync(t => t.Id == evaluacionesModel.IdConfiguracionTest);
            evaluacionesModel.Secciones = await _context.Secciones
                .Where(s => s.IdEvaluaciones == evaluacionesModel.Id && s.Eliminado == false)
                .ToListAsync();
            foreach (SeccionesModel seccion in evaluacionesModel.Secciones)
            {
                seccion.ConfiguracionSecciones = await _context.ConfiguracionesSecciones
                    .FirstOrDefaultAsync(cs => cs.Id == seccion.IdConfiguracionSecciones);
                seccion.Preguntas = await _context.Preguntas
                    .Where(p => p.IdSecciones == seccion.Id && p.Eliminado == false)
                    .ToListAsync();
                foreach (PreguntasModel pregunta in seccion.Preguntas)
                {
                    pregunta.ConfiguracionPreguntas = await _context.ConfiguracionesPreguntas
                        .FirstOrDefaultAsync(cp => cp.Id == pregunta.IdConfiguracionPreguntas);
                    pregunta.Opciones = await _context.Opciones
                        .Where(o => o.IdPreguntas == pregunta.Id && o.Eliminado == false)
                        .OrderBy(o => o.Orden)
                        .ToListAsync();
                }
            }


            return evaluacionesModel;
        }

        [HttpGet("paciente/{idPaciente}")]
        public async Task<ActionResult<IEnumerable<EvaluacionesModel>>> GetEvaluacionesByPaciente(int idPaciente)
        {
            var evaluacionesModel = await _context.Evaluaciones.
                Where(e=> e.IdPaciente==idPaciente && e.Eliminado== false)
                .OrderByDescending(e=> e.Creado)
                .ToListAsync();

            if (evaluacionesModel == null)
            {
                return NotFound();
            }


            return evaluacionesModel;
        }

        [HttpGet("paciente-activo/{idPaciente}")]
        public async Task<ActionResult<IEnumerable<EvaluacionesModel>>> GetEvaluacionesByPacienteActivas(int idPaciente)
        {
            ICollection<EvaluacionesModel> evaluacionesModel = await _context.Evaluaciones.
                Where(e => e.IdPaciente == idPaciente && e.Eliminado == false && e.Completado == false)
                .OrderByDescending(e => e.Creado)
                .ToListAsync();

            if (evaluacionesModel == null || !evaluacionesModel.Any())
            {
                return NotFound();
            }

            foreach (EvaluacionesModel eval in evaluacionesModel)
            {
                eval.ConfiguracionTest = await _context.ConfiguracionesTest.Include(t=> t.TipoTest)
                    .FirstOrDefaultAsync(t => t.Id == eval.IdConfiguracionTest);
                eval.Secciones = await _context.Secciones
                    .Where(s => s.IdEvaluaciones == eval.Id && s.Eliminado == false)
                    .ToListAsync();
                foreach (SeccionesModel seccion in eval.Secciones)
                {
                    seccion.ConfiguracionSecciones = await _context.ConfiguracionesSecciones
                        .FirstOrDefaultAsync(cs => cs.Id == seccion.IdConfiguracionSecciones);
                    seccion.Preguntas = await _context.Preguntas
                        .Where(p => p.IdSecciones == seccion.Id && p.Eliminado == false)
                        .ToListAsync();
                    foreach (PreguntasModel pregunta in seccion.Preguntas)
                    {
                        pregunta.ConfiguracionPreguntas= await _context.ConfiguracionesPreguntas
                            .FirstOrDefaultAsync(cp => cp.Id == pregunta.IdConfiguracionPreguntas);
                        pregunta.Opciones = await _context.Opciones
                            .Where(o => o.IdPreguntas == pregunta.Id && o.Eliminado == false)
                            .OrderBy(o => o.Orden)
                            .ToListAsync();
                    }
                }
            }

            return Ok(evaluacionesModel);
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
            if (!EvaluacionesExists(id))
            {
                return NotFound();
            }
            evaluacionesModel.Actualizado = DateTime.Now;

            _context.Evaluaciones.Update(evaluacionesModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Evaluacion/generar-evaluacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("generar-evaluacion")]
        public async Task<ActionResult<EvaluacionesModel>> GenerarEvaluaciones(GenerarEvaluacionViewModel datosEvaluacion)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                EvaluacionesModel evaluacionesModel = new EvaluacionesModel();
                ConfiguracionTestModel configuracionEvaluacion = new ConfiguracionTestModel();
                if (datosEvaluacion != null)
                {
                    //cargar Evaluacion de plantilla
                    configuracionEvaluacion = await _context.ConfiguracionesTest
                        .FirstOrDefaultAsync(t => t.Id == datosEvaluacion.IdConfiguracionTest);
                    if (configuracionEvaluacion != null)
                    {
                        evaluacionesModel.Evaluacion = configuracionEvaluacion.Nombre;
                        evaluacionesModel.IdEvaluador = datosEvaluacion.IdEvaluador;
                        evaluacionesModel.IdPaciente = datosEvaluacion.IdPaciente;
                        evaluacionesModel.IdConfiguracionTest = datosEvaluacion.IdConfiguracionTest;
                        evaluacionesModel.Duracion = configuracionEvaluacion.Duracion;

                        //Obtener secciones

                        configuracionEvaluacion.ConfiguracionesSecciones = await _context.ConfiguracionesSecciones
                            .Where(s => s.IdConfiguracionesTest == datosEvaluacion.IdConfiguracionTest && s.Eliminado == false)
                            .ToListAsync();
                        ICollection<ConfiguracionPreguntasModel> preguntasAleatorias = [];
                        evaluacionesModel.Secciones = new List<SeccionesModel>();
                        int Orden = 1;
                        foreach (ConfiguracionSeccionesModel seccionConfig in configuracionEvaluacion.ConfiguracionesSecciones)
                        {
                            SeccionesModel seccionModel = new SeccionesModel();
                            seccionModel.Seccion = seccionConfig.Seccion;
                            seccionModel.IdConfiguracionSecciones = seccionConfig.Id;
                            seccionModel.FormulaAgregado = seccionConfig.FormulaAgregado;
                            seccionConfig.BancoPreguntas = await _context.ConfiguracionesPreguntas
                                .Where(p => p.IdConfiguracionSecciones == seccionConfig.Id && p.Eliminado == false)
                                .ToListAsync();

                            // Seleccionar aleatoriamente las preguntas necesarias de la sección, sin repetir
                            if (seccionConfig.NumeroPreguntas <= seccionConfig.BancoPreguntas.Count)
                            {
                                seccionModel.Preguntas = new List<PreguntasModel>();
                                // Mezclar aleatoriamente el banco de preguntas
                                preguntasAleatorias = seccionConfig.BancoPreguntas
                                    .OrderBy(x => Guid.NewGuid())
                                    .Take(seccionConfig.NumeroPreguntas)
                                    .ToList();
                                if (preguntasAleatorias.Count == 0)
                                {
                                    await transaction.RollbackAsync();
                                    return BadRequest(new
                                    {
                                        message = "Error en generar las preguntas aleatoriamente"
                                    });
                                }
                                foreach (ConfiguracionPreguntasModel preguntasConfig in preguntasAleatorias)
                                {
                                    PreguntasModel preguntaModel = new PreguntasModel();
                                    preguntaModel.Pregunta = preguntasConfig.Pregunta;
                                    preguntaModel.Orden = Orden;
                                    preguntaModel.IdConfiguracionPreguntas = preguntasConfig.Id;
                                    preguntasConfig.Opciones = await _context.ConfiguracionesOpciones
                                        .Where(o => o.IdConfiguracionPreguntas == preguntasConfig.Id && o.Eliminado == false)
                                        .OrderBy(o => o.Orden)
                                        .ToListAsync();
                                    if (preguntasConfig.Opciones.Count == 0)
                                    {
                                        await transaction.RollbackAsync();
                                        return UnprocessableEntity(new
                                        {
                                            message = "Las pregunta '" + preguntaModel.Pregunta + "' no tienen opciones para continuar " + seccionConfig.Seccion + " para generar la evaluacion"
                                        });
                                    }
                                    foreach (ConfiguracionOpcionesModel opcionesConfig in preguntasConfig.Opciones)
                                    {
                                        OpcionesModel opcionesModel = new OpcionesModel();
                                        opcionesModel.Opcion = opcionesConfig.Opcion;
                                        opcionesModel.Orden = opcionesConfig.Orden;
                                        opcionesModel.Peso = opcionesConfig.Peso;
                                        preguntaModel.Opciones.Add(opcionesModel);
                                    }
                                    seccionModel.Preguntas.Add(preguntaModel);
                                    Orden++;
                                }
                            }
                            else
                            {
                                await transaction.RollbackAsync();
                                return UnprocessableEntity(new
                                {
                                    message = "No existe la cantidad de preguntas " + seccionConfig.Seccion + " para generar la evaluacion"
                                });
                            }
                            evaluacionesModel.Secciones.Add(seccionModel);

                        }
                        evaluacionesModel.CantidadPreguntas = Orden - 1;
                        if (evaluacionesModel.CantidadPreguntas == 0)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest(new
                            {
                                message = "Error en generar la evaluación no exiten preguntas para esta evaluación"
                            });
                        }
                        evaluacionesModel.NoContestadas = evaluacionesModel.CantidadPreguntas;
                        evaluacionesModel.ConfiguracionTest = configuracionEvaluacion;

                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new
                        {
                            message = "Error en generar la evaluación no exiten configuración para esta evaluación"
                        });
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    return UnprocessableEntity(new
                    {
                        message = "No se envia la evaluacion"
                    });
                }

                _context.Evaluaciones.Add(evaluacionesModel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return evaluacionesModel;
                //return CreatedAtAction("GetEvaluacionesModel", new { id = evaluacionesModel.Id }, evaluacionesModel);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }



        // POST: api/EvaluacionModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluacionesModel>> PostEvaluaciones(EvaluacionesModel evaluacionesModel)
        {
            _context.Evaluaciones.Add(evaluacionesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluacionesModel", new { id = evaluacionesModel.Id }, evaluacionesModel);
        }

        // DELETE: api/EvaluacionModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluaciones(int id)
        {
            var evaluacionesModel = await _context.Evaluaciones.FindAsync(id);
            if (evaluacionesModel == null)
            {
                return NotFound();
            }
            evaluacionesModel.Eliminado = true;
            evaluacionesModel.Actualizado = DateTime.Now;
            _context.Evaluaciones.Update(evaluacionesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluacionesExists(int id)
        {
            return _context.Evaluaciones.Any(e => e.Id == id);
        }
    }
}
