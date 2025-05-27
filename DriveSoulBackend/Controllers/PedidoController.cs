using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DriveSoulBackend.Data;
using DriveSoulBackend.Entities;
using DriveSoulBackend.DTO.PedidosDTO;

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListarPedidoDTO>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidoItems)
                    .ThenInclude(pi => pi.Producto)
                .Select(p => new ListarPedidoDTO
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    NombreUsuario = p.Usuario.Nombre + " " + p.Usuario.Apellido,
                    ImporteTotal = p.ImporteTotal,
                    Estado = p.Estado,
                    DireccionEnvio = p.DireccionEnvio,
                    MetodoPago = p.MetodoPago,
                    EstadoPago = p.EstadoPago,
                    FechaCreacion = p.FechaCreacion,
                    FechaActualizacion = p.FechaActualizacion,
                    Items = p.PedidoItems.Select(pi => new PedidoItemDTO
                    {
                        Id = pi.Id,
                        ProductoId = pi.ProductoId,
                        NombreProducto = pi.Producto.Nombre,
                        Cantidad = pi.Cantidad,
                        Precio = pi.Precio,
                        FechaCreacion = pi.FechaCreacion
                    }).ToList()
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        // GET: api/pedido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListarPedidoDTO>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidoItems)
                    .ThenInclude(pi => pi.Producto)
                .Where(p => p.Id == id)
                .Select(p => new ListarPedidoDTO
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    NombreUsuario = p.Usuario.Nombre + " " + p.Usuario.Apellido,
                    ImporteTotal = p.ImporteTotal,
                    Estado = p.Estado,
                    DireccionEnvio = p.DireccionEnvio,
                    MetodoPago = p.MetodoPago,
                    EstadoPago = p.EstadoPago,
                    FechaCreacion = p.FechaCreacion,
                    FechaActualizacion = p.FechaActualizacion,
                    Items = p.PedidoItems.Select(pi => new PedidoItemDTO
                    {
                        Id = pi.Id,
                        ProductoId = pi.ProductoId,
                        NombreProducto = pi.Producto.Nombre,
                        Cantidad = pi.Cantidad,
                        Precio = pi.Precio,
                        FechaCreacion = pi.FechaCreacion
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // GET: api/pedido/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ListarPedidoDTO>>> GetPedidosPorUsuario(int usuarioId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidoItems)
                    .ThenInclude(pi => pi.Producto)
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => new ListarPedidoDTO
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    NombreUsuario = p.Usuario.Nombre + " " + p.Usuario.Apellido,
                    ImporteTotal = p.ImporteTotal,
                    Estado = p.Estado,
                    DireccionEnvio = p.DireccionEnvio,
                    MetodoPago = p.MetodoPago,
                    EstadoPago = p.EstadoPago,
                    FechaCreacion = p.FechaCreacion,
                    FechaActualizacion = p.FechaActualizacion,
                    Items = p.PedidoItems.Select(pi => new PedidoItemDTO
                    {
                        Id = pi.Id,
                        ProductoId = pi.ProductoId,
                        NombreProducto = pi.Producto.Nombre,
                        Cantidad = pi.Cantidad,
                        Precio = pi.Precio,
                        FechaCreacion = pi.FechaCreacion
                    }).ToList()
                })
                .ToListAsync();

            return Ok(pedidos);
        }
    }
} 