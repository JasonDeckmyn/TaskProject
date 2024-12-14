using aspnet.Models;
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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
    dbContext.Database.Migrate();
    SeedDB(dbContext);
}

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


void SeedDB(TodoContext context)
{
    if(!context.Todos.Any())
    {
       var todos = new List<Todo>
        {
            new Todo { Title = "Learn C#", Description = "Learn the basics of C#", Urgency = UrgencyLevel.High },
            new Todo { Title = "Learn ASP.NET", Description = "Learn how to build web applications with ASP.NET", Urgency = UrgencyLevel.Medium },
            new Todo { Title = "Learn EF Core", Description = "Learn how to use Entity Framework Core", Urgency = UrgencyLevel.High },
            new Todo { Title = "Learn Azure", Description = "Learn how to deploy applications to Azure", Urgency = UrgencyLevel.Low }
        };

        context.Todos.AddRange(todos);
        context.SaveChanges();
    }
}