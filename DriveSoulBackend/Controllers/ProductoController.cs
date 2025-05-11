using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Asegúrate de agregar esta línea
using System.Collections.Generic;
using System.Threading.Tasks;

using DriveSoulBackend.DTO.ProductosDTO; // Asegúrate de tener el espacio de nombres correcto para ListarProducto
using DriveSoulBackend.DTO.CochesDTO; // Asegúrate de tener el espacio de nombres correcto para ListarProducto
using DriveSoulBackend.Entities;  // Asegúrate de tener el espacio de nombres correcto para Producto
using DriveSoulBackend.Data;

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<ProductoController>
        // Para usar el postman La dirección es http://localhost:5000/api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListarProductoDTO>>> GetProductos()
        {
            var productos = await _context.Productos
                .Select(p => new ListarProductoDTO // Con Select(...), convierte cada entidad Producto en un DTO ListarProducto
                {
                    id= p.id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,          
                    Categoria = p.Categoria
                })
                .ToListAsync(); // trae todos los productos de la base de datos.
            return Ok(productos);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ListarProductoDTO>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Where(p => p.id == id)
                .Select(p => new ListarProductoDTO
                {
                    id = p.id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Categoria = p.Categoria
                })
                .FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, CrearProductoDto editarProductoDto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return NotFound(new { mensaje = $"No se encontró un producto con ID {id}" });
            }

            // Actualizar campos
            productoExistente.Nombre = editarProductoDto.Nombre ?? productoExistente.Nombre;
            productoExistente.Descripcion = editarProductoDto.Descripcion ?? productoExistente.Descripcion;
            productoExistente.Precio = editarProductoDto.Precio != 0 ? editarProductoDto.Precio : productoExistente.Precio;
            productoExistente.Categoria = editarProductoDto.Categoria ?? productoExistente.Categoria;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Producto actualizado correctamente", producto = productoExistente });
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el producto", detalles = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(CrearProductoDto crearProductoDto)
        {
            // Validar si el id ya existe
            var existe = await _context.Productos.AnyAsync(p => p.id == crearProductoDto.id);
            if (existe)
            {
                return BadRequest(new { mensaje = $"Ya existe un producto con el ID {crearProductoDto.id}" });
            }

            var nuevoProducto = new Producto
            {
                id = crearProductoDto.id, // <-- solo si el cliente lo manda
                Nombre = crearProductoDto.Nombre,
                Descripcion = crearProductoDto.Descripcion,
                Precio = crearProductoDto.Precio,
                Categoria = crearProductoDto.Categoria
            };

            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = nuevoProducto.id }, nuevoProducto);
        }


    }
}
