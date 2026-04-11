using Microsoft.EntityFrameworkCore;
using PlatformsService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add the database context to the service container, using an in-memory database for simplicity
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMem"));

// Register the repository for dependency injection
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
}


app.Run();

