using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // <-- Aseg�rate de agregar esta l�nea
using System.Collections.Generic;
using System.Threading.Tasks;
using DriveSoulBackend.DTO.CochesDTO; // Aseg�rate de tener el espacio de nombres correcto para ListarProducto
using DriveSoulBackend.Entities;  // Aseg�rate de tener el espacio de nombres correcto para Producto
using DriveSoulBackend.Data;
using System.Linq;

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
        // Para usar el postman La direcci�n es http://localhost:5000/api/Producto
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
            var productosConCoches = await (from p in _context.Productos
                                            join c in _context.Coches on p.id equals c.producto_id
                                            join modelo in _context.Modelos on c.modelo_id equals modelo.id
                                            join marca in _context.Marcas on modelo.marca_id equals marca.id
                                            select new ListarProductoCocheDTO
                                            {
                                                producto_id = p.id,
                                                nombre = p.Nombre,
                                                descripcion = p.Descripcion,
                                                precio = p.Precio,
                                                categoria = p.Categoria,

                                                modelo_id = c.modelo_id,
                                                anio = c.anio,
                                                kilometraje = c.kilometraje,
                                                color = c.color,
                                                tipo_combustible = c.tipo_combustible,
                                                transmision = c.transmision,

                                                modeloNombre = modelo.nombre,
                                                marcaNombre = marca.nombre
                                            }).ToListAsync();

            // Obtener las imágenes para cada coche
            foreach (var coche in productosConCoches)
            {
                var imagenes = await _context.Imagenes
                    .Where(i => i.ProductoId == coche.producto_id)
                    .Select(i => i.Url)
                    .ToListAsync();
                
                coche.imagenes = imagenes;
            }

            return Ok(productosConCoches);
        }

    }
}
