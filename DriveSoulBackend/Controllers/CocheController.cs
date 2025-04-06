using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Asegúrate de agregar esta línea
using System.Collections.Generic;
using System.Threading.Tasks;
using DriveSoulBackend.DTO.CochesDTO; // Asegúrate de tener el espacio de nombres correcto para ListarProducto
using DriveSoulBackend.Entities;  // Asegúrate de tener el espacio de nombres correcto para Producto
using DriveSoulBackend.Data;

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocheController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CocheController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/<ProductoController>
        // Para usar el postman La dirección es http://localhost:5000/api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListarCochesDTO>>> GetCoches()
        {
            var coches = await _context.Coches
                .Select(p => new ListarCochesDTO // Con Select(...), convierte cada entidad Producto en un DTO ListarProducto
                {
                    producto_id = p.producto_id,
                    modelo_id = p.modelo_id,
                    anio = p.anio,
                    kilometraje = p.kilometraje,
                    color = p.color,
                    tipo_combustible = p.tipo_combustible,
                    transmision = p.transmision
                })
                .ToListAsync(); // trae todos los productos de la base de datos.

            return Ok(coches);

        }
        [HttpGet("ListarCoches")]
        public async Task<ActionResult<IEnumerable<ListarProductoCocheDTO>>> GetCochesConDatosDelProducto()
        {
            var productosConCoches = await _context.Productos
                .Join(_context.Coches,
                      p => p.id,           // Relacionamos Producto.Id con CocheEntity.ProductoId
                      c => c.producto_id,  // ProductoId es la clave foránea en CocheEntity
                      (p, c) => new ListarProductoCocheDTO
                      {
                          nombre = p.Nombre,         // Usa p.Nombre en lugar de p.nombre
                          descripcion = p.Descripcion, // Usa p.Descripcion en lugar de p.descripcion
                          precio = p.Precio,          // Usa p.Precio en lugar de p.precio
                          categoria = p.Categoria,    // Usa p.Categoria en lugar de p.categoria

                          // Datos de la tabla Coches
                          modelo_id = c.modelo_id,
                          anio = c.anio,
                          kilometraje = c.kilometraje,
                          color = c.color,
                          tipo_combustible = c.tipo_combustible,
                          transmision = c.transmision
                      })
                .ToListAsync();

            return Ok(productosConCoches);
        }

    }
}
