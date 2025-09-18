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

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(int id, UsuarioModel usuarioModel)
        {
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
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(UsuarioModel usuarioModel)
        {
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

            _context.Usuarios.Remove(usuarioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioModelExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
