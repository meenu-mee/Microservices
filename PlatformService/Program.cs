using Microsoft.EntityFrameworkCore;
using PlatformsService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add the database context to the service container, using an in-memory database for simplicity
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMem"));

builder.Services.AddControllers();
// Register AutoMapper with the service container, scanning the current assembly for mapping profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register the repository for dependency injection
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();  
app.UseRouting();
app.UseAuthorization(); 

// Map controller routes and ensure the database is populated with initial data when the application starts
app.MapControllers();
PrepDb.PrepPopulation(app); 


app.Run();

