using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskProject.Contexts;
using TaskProject.Repostitories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRepo, MysqlRepo>(); //MockRepo can be switched with MySQL later on

string _connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ToDoContext>(options => options.UseMySql(_connectionstring, ServerVersion.AutoDetect(_connectionstring)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();