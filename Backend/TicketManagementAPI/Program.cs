using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketManagementAPI;
using TicketManagementAPI.Models;
using TicketManagementAPI.Repositories;
using TicketManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Register FluentValidation
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()  // Enable automatic validation
                .AddValidatorsFromAssemblyContaining<TicketValidator>(); // Register validators

// Register the DbContext with your cloud SQL server
builder.Services.AddDbContext<TicketContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  // For SQL Server in the cloud

// Dependency injection for repository and services
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();

// Configure CORS if needed
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ticket Management API",
        Description = "A simple API to manage tickets"
    });
});

// Use specific URLs
builder.WebHost.UseUrls("https://localhost:5000");

var app = builder.Build();

// Use CORS
app.UseCors("AllowAllOrigins");

// Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Management API v1");
        c.RoutePrefix = string.Empty; // Swagger available at root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
