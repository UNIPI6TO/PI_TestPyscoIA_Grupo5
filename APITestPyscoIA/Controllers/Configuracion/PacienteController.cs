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
    public class PacienteController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public PacienteController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/config/Paciente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteModel>>> GetPacientes()
        {
            return await _context.Pacientes
                .Where(p => p.Eliminado==false || p.Eliminado==null )
                .Include(p => p.Ciudad)
                .OrderBy(p => p.Nombre).ToListAsync();
        }

        // GET: api/config/Paciente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteModel>> GetPacienteModel(int id)
        {
            var pacienteModel = await _context.Pacientes.FindAsync(id);

            if (pacienteModel == null)
            {
                return NotFound();
            }

            return pacienteModel;
        }

        // PUT: api/config/Paciente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacienteModel(int id, PacienteModel pacienteModel)
        {
            if (id != pacienteModel.Id)
            {
                return BadRequest();
            }
            pacienteModel.Actualizado = DateTime.Now;
            _context.Entry(pacienteModel).State = EntityState.Modified;

            try
            {
               
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pacienteModel);
        }

        // POST: api/config/Paciente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PacienteModel>> PostPacienteModel(PacienteDTO pacienteDTO)
        {
            PacienteModel pacienteModel = new PacienteModel
            {
                Cedula = pacienteDTO.Cedula,
                Nombre = pacienteDTO.Nombre,
                Email = pacienteDTO.Email,
                FechaNacimiento = pacienteDTO.FechaNacimiento,
                Direccion = pacienteDTO.Direccion,
                IdCiudad = pacienteDTO.IdCiudad,
                Creado = DateTime.UtcNow,
                Eliminado = false,
                Sincronizado = false
            };
            _context.Pacientes.Add(pacienteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacienteModel", new { id = pacienteModel.Id }, pacienteModel);
        }

        // DELETE: api/config/Paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacienteModel(int id)
        {

            var pacienteModel = await _context.Pacientes.FindAsync( id);
            if (pacienteModel == null)
                return BadRequest();

            pacienteModel.Actualizado = DateTime.Now;
            pacienteModel.Eliminado = true;
            _context.Entry(pacienteModel).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pacienteModel);
        }

        private bool PacienteModelExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
