using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Required for Swagger/OpenAPI
using TaskProject.Contexts;
using TaskProject.Repostitories;

var builder = WebApplication.CreateBuilder(args);

//
//
// VIDEO API ENDED 1:12:06
//
//


// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI (use AddSwaggerGen for OpenAPI support)
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

builder.Services.AddScoped<IRepo, MysqlRepo>(); // MockRepo can be switched with MySQL later on

// Ensure the configuration is loaded
var configuration = builder.Configuration;
string _connectionstring = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring)));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
