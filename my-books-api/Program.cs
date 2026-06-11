using Microsoft.EntityFrameworkCore;
using my_books_api.Data;
using my_books_api.Data.Services;
using my_books_api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// SERILOG
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/app-log.txt",
            rollingInterval: RollingInterval.Day);
});

// Conexión SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnectionString")
    ));

// Controllers
builder.Services.AddControllers();

// API Versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Services
builder.Services.AddTransient<BooksService>();
builder.Services.AddTransient<AuthorsService>();
builder.Services.AddTransient<PublishersService>();
builder.Services.AddTransient<LogsService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// LOGGER FACTORY (esto reemplaza Startup.cs)
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Exception Handling
app.ConfigureBuildInExceptionHandler(loggerFactory);

// Controllers
app.MapControllers();

// Redirect Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();