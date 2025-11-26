using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using my_books_api.Data;
using my_books_api.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexión a la base de datos SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Agregar controladores
builder.Services.AddControllers();

// Configure Services
builder.Services.AddTransient<BooksService>();

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

AppDbInitializer.Seed(app);

app.UseHttpsRedirection();

// Mapea los controllers 👈 NECESARIO
app.MapControllers();

// Redirige al Swagger por defecto
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
