using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Asegúrate de agregar esta línea
using System.Collections.Generic;
using System.Threading.Tasks;
using DriveSoulBackend.Entities;  // Asegúrate de tener el espacio de nombres correcto para Producto
using DriveSoulBackend.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
// Añadir referencia a los DTOs
using DriveSoulBackend.Dtos;

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //private readonly IConfiguration _config;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<Usuarios>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // POST api/<UsuariosController>

        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuario(Usuarios usuario)
        {
            // Verificar si el email ya existe en la base de datos
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
            if (usuarioExistente != null)
            {
                return BadRequest(new { mensaje = "El email ya está registrado" });
            }

            // Hashear la contraseña antes de guardarla
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            var newUser = new Usuarios
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Ciudad = usuario.Ciudad,
                Password = passwordHash, 
                Rol = usuario.Rol ?? "Usuario" 
            };

            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = newUser.Id }, newUser);
        }

        // GetLogin 
        //[HttpGet]
        //public IActionResult LoginUser()
        //{
        //    var UsuarioId = User.FindFirst(ClaimTypes.NameIdentifier) ? ValueTask
        //    return Ok(new {mensaje = "Bienvenido", UsuarioId})

        //}

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Identificador) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new { mensaje = "Datos de login incompletos" });
            }

            // Buscar usuario por nombre o email
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == loginDto.Identificador || u.Email == loginDto.Identificador);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            // Verificar contraseña
            bool passwordValida = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password);
            if (!passwordValida)
            {
                return Unauthorized(new { mensaje = "Contraseña incorrecta" });
            }

            // Devolver datos del usuario (excepto la contraseña)
            return Ok(new
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Ciudad = usuario.Ciudad,
                Rol = usuario.Rol
            });
        }

    }
}
