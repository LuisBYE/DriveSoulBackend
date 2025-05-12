using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriveSoulBackend.Data;
using DriveSoulBackend.Entities;
using DriveSoulBackend.DTO.CarritoDTO;

namespace DriveSoulBackend.Controllers
{
    [Route("api/carrito")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/carrito/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<CarritoDTO>>> GetCarritosPorUsuario(int usuarioId)
        {
            var carritos = await _context.Carritos
                .Where(c => c.UsuarioId == usuarioId)
                .Select(c => new CarritoDTO
                {
                    Id = c.Id,
                    UsuarioId = c.UsuarioId,
                    ProductoId = c.ProductoId,
                    Cantidad = c.Cantidad,
                    FechaCreacion = c.FechaCreacion,
                    FechaActualizacion = c.FechaActualizacion
                })
                .ToListAsync();

            return Ok(carritos);
        }

        // GET: api/carrito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
                return NotFound();

            var dto = new CarritoDTO
            {
                Id = carrito.Id,
                UsuarioId = carrito.UsuarioId,
                ProductoId = carrito.ProductoId,
                Cantidad = carrito.Cantidad,
                FechaCreacion = carrito.FechaCreacion,
                FechaActualizacion = carrito.FechaActualizacion
            };

            return Ok(dto);
        }

        // POST: api/carrito
        [HttpPost]
        public async Task<ActionResult<CarritoDTO>> PostCarrito(CarritoDTO dto)
        {
            if (dto.UsuarioId <= 0 || dto.ProductoId <= 0 || dto.Cantidad <= 0)
                return BadRequest("Datos del carrito no válidos.");

            var carrito = new CarritoEntity
            {
                UsuarioId = dto.UsuarioId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            dto.Id = carrito.Id;
            dto.FechaCreacion = carrito.FechaCreacion;

            return CreatedAtAction(nameof(GetCarrito), new { id = carrito.Id }, dto);
        }

        // PUT: api/carrito/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, CarritoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID del carrito no coincide.");

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
                return NotFound();

            carrito.UsuarioId = dto.UsuarioId;
            carrito.ProductoId = dto.ProductoId;
            carrito.Cantidad = dto.Cantidad;
            carrito.FechaActualizacion = DateTime.UtcNow;

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al actualizar el carrito.");
            }
        }

        // DELETE: api/carrito/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
                return NotFound();

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/carrito/usuario/5
        [HttpDelete("usuario/{usuarioId}")]
        public async Task<IActionResult> DeleteCarritoPorUsuario(int usuarioId)
        {
            var items = await _context.Carritos
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            if (!items.Any())
                return NotFound("No se encontraron ítems para este usuario.");

            _context.Carritos.RemoveRange(items);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
