using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Asegúrate de agregar esta línea
using System.Collections.Generic;
using System.Threading.Tasks;
using DriveSoulBackend.DTO.ProductosDTO; // Asegúrate de tener el espacio de nombres correcto para ListarProducto
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
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Categoria = p.Categoria
                })
                .ToListAsync(); // trae todos los productos de la base de datos.
            return Ok(productos);


        }


        // GET api/<ProductoController>/
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Producto>> GetProducto(int id)
        //{
        //    var producto = await _context.Productos.FindAsync(id);

        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(producto);
        //}

    }
}
