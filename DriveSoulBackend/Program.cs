using Microsoft.EntityFrameworkCore; // Asegúrate de tener este using
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Asegúrate de tener este using
using DriveSoulBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Reemplaza con el origen de tu frontend
                  .AllowAnyMethod() // o especifica métodos concretos (GET, POST, PUT, DELETE)
                  .AllowAnyHeader();
        });
});
// Añadir Controladores
builder.Services.AddControllers();
// https Forzado
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.HttpsPort = 7239; // Especifica el puerto HTTPS aquí (como está configurado en launchSettings.json)
//});

// Configuración de la conexión a la base de datos con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "YourIssuer",  // Cambia a tu emisor
            ValidAudience = "YourAudience",  // Cambia a tu audiencia
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))  // Cambia a tu clave secreta
        };
    });

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // El valor predeterminado de HSTS es de 30 días. Puedes cambiarlo para escenarios de producción.
    app.UseHsts();
}
builder.Services.AddAuthorization();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowMyOrigin");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // DEJA SOLO ESTA LÍNEA
});

app.MapRazorPages();

app.Run();