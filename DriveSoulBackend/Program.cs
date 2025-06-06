using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.OpenApi.Models;
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
            policy.WithOrigins("http://localhost:3000") // Reempl�zalo con el origen de tu frontend
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// A�adir Controladores
builder.Services.AddControllers();

// Configuraci�n de Swagger con documentaci�n mejorada
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DriveSoul API",
        Version = "v1",
        Description = "API para gestionar DriveSoul",
        Contact = new OpenApiContact
        {
            Name = "Luis",
            Email = "luis_aleixsanchez@ceroca.com",
            Url = new Uri("https://DriveSoul.com")
        }
    });
});

// Configuraci�n de la conexi�n a MySQL
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

// Configuraci�n del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DriveSoul API v1");
        c.RoutePrefix = "swagger"; // Hace que Swagger UI est� disponible en "/swagger"
    });
}

app.UseStaticFiles();
app.UseCors("AllowMyOrigin");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

