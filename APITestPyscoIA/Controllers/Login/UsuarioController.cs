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

namespace APITestPyscoIA.Controllers.Login
{
    public class RegisterPerfilPacienteViewModel
    {
        public string Cedula { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public UsuarioController(DatosDbContext context)
        {
            _context = context;
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioModel>> PostIniciarSesion(UsuarioViewModel usuario)
        {
           if (usuario == null || string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Password))
            {
                return BadRequest(new {message= "El usuario y la contraseña son obligatorios." });
            }

            UsuarioModel usuarioModel= await _context.Usuarios
                                    .Where(u => u.Usuario == usuario.Usuario && u.Password == usuario.Password && u.Eliminado==false)
                                    .FirstOrDefaultAsync();
            if (usuarioModel == null)
            {
                return NotFound(new { message ="Credenciales Inválidas" });
            }
            return usuarioModel;
        }
        [HttpPost("RegistrarPerfilPaciente")]
        public async Task<IActionResult> RegistrarPerfilPaciente(RegisterPerfilPacienteViewModel request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Cedula) ||
                string.IsNullOrWhiteSpace(request.Usuario) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Cédula, Usuario y Contraseña son obligatorios." });
            }

            // 1. Buscar paciente por cédula
            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Cedula == request.Cedula);

            if (paciente == null)
            {
                return BadRequest(new { message = "No existe un paciente con esa cédula. Solicite al administrador registrarlo primero." });
            }

            // 2. Verificar si ya tiene perfil de usuario
            var existeUsuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.idPaciente == paciente.Id && u.Eliminado == false);

            if (existeUsuario != null)
            {
                return BadRequest(new { message = "Ya existe un perfil de usuario para este paciente." });
            }

            // 3. Crear el usuario asociado al paciente (contraseña en texto plano)
            var nuevoUsuario = new UsuarioModel
            {
                Usuario = request.Usuario,
                Password = request.Password,
                Rol = "PACIENTE",
                idPaciente = paciente.Id,
                Creado = DateTime.Now,
                Eliminado = false
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Perfil creado correctamente. Ya puede iniciar sesión.", id = nuevoUsuario.Id });
        }


        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }
        [HttpGet("Existe/{usuario}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioByEvaluadorModel(string usuario)
        {
            var usuarioModel = await _context.Usuarios
                .Where(u => u.Usuario == usuario)
                .FirstOrDefaultAsync();

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // GET: api/Usuario/5
        [HttpGet("Evaluador/{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioByEvaluadorModel(int id)
        {
            var usuarioModel = await _context.Usuarios
                .Where(u=> u.idEvaluador==id)
                .FirstOrDefaultAsync();

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // GET: api/Usuario/5
        [HttpGet("Paciente/{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioByPacienteModel(int id)
        {
            var usuarioModel = await _context.Usuarios
                .Where(u => u.idPaciente == id)
                .FirstOrDefaultAsync();

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(int id, CrearUsuarioViewModel usuario)
        {
            UsuarioModel usuarioModel = new UsuarioModel
            {
                Id = id,
                Usuario = usuario.Usuario,
                Password = usuario.Password,
                Rol = usuario.Rol,
                idEvaluador = usuario.idEvaluador,
                idPaciente = usuario.idPaciente,
                Actualizado = DateTime.Now,
                Creado = usuario.Creado,
                Eliminado = usuario.Eliminado

            };

            if (id != usuarioModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuarioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(id))
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

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(CrearUsuarioViewModel usuario)
        {
            UsuarioModel usuarioModel = new UsuarioModel
            {
                Usuario = usuario.Usuario,
                Password = usuario.Password,
                Rol = usuario.Rol,
                idEvaluador = usuario.idEvaluador,
                idPaciente = usuario.idPaciente,
                Creado= DateTime.Now,
                Eliminado = false
            };
            _context.Usuarios.Add(usuarioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioModel", new { id = usuarioModel.Id }, usuarioModel);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }
            usuarioModel.Eliminado = false;
            usuarioModel.Actualizado = DateTime.Now;
            _context.Usuarios.Update(usuarioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioModelExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
