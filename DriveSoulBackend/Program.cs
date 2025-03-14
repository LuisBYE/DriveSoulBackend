using Microsoft.EntityFrameworkCore; // Aseg�rate de tener este using
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Aseg�rate de tener este using
using DriveSoulBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Reemplaza con el origen de tu frontend
                  .AllowAnyMethod() // o especifica m�todos concretos (GET, POST, PUT, DELETE)
                  .AllowAnyHeader();
        });
});
// A�adir Controladores
builder.Services.AddControllers();
// https Forzado
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.HttpsPort = 7239; // Especifica el puerto HTTPS aqu� (como est� configurado en launchSettings.json)
//});

// Configuraci�n de la conexi�n a la base de datos con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // El valor predeterminado de HSTS es de 30 d�as. Puedes cambiarlo para escenarios de producci�n.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowMyOrigin");
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // DEJA SOLO ESTA L�NEA
});

app.MapRazorPages();

app.Run();