using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Asegúrate de agregar esta línea
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();  // Ahora ToListAsync debe funcionar
            return Ok(productos);
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // Otros métodos (POST, PUT, DELETE)...
    }
}
