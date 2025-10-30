using Microsoft.EntityFrameworkCore;
using my_books_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexión a la base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Agregar controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Redirige al Swagger por defecto
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
