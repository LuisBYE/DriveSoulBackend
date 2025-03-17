//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IConfiguration _config; // Para acceder a la configuración (JWT)

//    public AuthController(ApplicationDbContext context, IConfiguration config)
//    {
//        _context = context;
//        _config = config;
//    }

//    [HttpPost("login")]
//    public async Task<IActionResult> Login([FromBody] LoginDTO login)
//    {
//        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == login.Email);

//        if (usuario == null || !VerificarPassword(login.Password, usuario.PasswordHash))
//        {
//            return Unauthorized(new { mensaje = "Credenciales incorrectas" });
//        }

//        // Si las credenciales son correctas, generamos el JWT
//        var token = GenerarToken(usuario);
//        return Ok(new { token });
//    }

//    private bool VerificarPassword(string password, string passwordHash)
//    {
//        return BCrypt.Net.BCrypt.Verify(password, passwordHash); // Usa BCrypt para verificar la contraseña
//    }

//    private string GenerarToken(Usuario usuario)
//    {
//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//        var claims = new[]
//        {
//            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
//            new Claim(ClaimTypes.Email, usuario.Email)
//        };

//        var token = new JwtSecurityToken(
//            _config["Jwt:Issuer"],
//            _config["Jwt:Audience"],
//            claims,
//            expires: DateTime.UtcNow.AddHours(1),
//            signingCredentials: creds
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}
