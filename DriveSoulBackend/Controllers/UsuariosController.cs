using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DriveSoulBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;

        // Constructor
        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
            // Puedes usar Console.WriteLine en el constructor si lo deseas
            Console.WriteLine("Prueba"); // Esto se ejecutará cuando se cree la instancia del controlador.
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            // Si usas un logger, puedes hacer algo así:
            _logger.LogInformation("RUTA ENCONTRADA");

            // O si solo quieres imprimir en consola sin usar logger:
            Console.WriteLine("RUTA ENCONTRADA");

            return Ok("Ruta de prueba exitosa");
        }
    }
}
