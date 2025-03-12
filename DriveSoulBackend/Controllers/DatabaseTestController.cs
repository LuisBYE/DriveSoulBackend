using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DriveSoulBackend.Data;  // Usar el namespace correcto de tu contexto de datos

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DatabaseTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("check-connection")]
        public IActionResult CheckConnection()
        {
            try
            {
                // Prueba la conexión con la base de datos
                _context.Database.CanConnect();
                return Ok(new { status = "OK", message = "Connection successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "Error", message = ex.Message });
            }
        }
    }
}
