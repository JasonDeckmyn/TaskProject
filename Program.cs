using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Required for Swagger/OpenAPI
using TaskProject.Contexts;
using TaskProject.Repostitories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI (add Swagger services here)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task API",
        Version = "v1",
        Description = "An API for managing tasks"
    });
});

// Register other services
builder.Services.AddScoped<IRepo, MysqlRepo>();

// Configure EF Core with MySQL
var configuration = builder.Configuration;
string _connectionstring = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring)));

// Add AutoMapper (if applicable)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Add Swagger middleware here
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
    });
}

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();
