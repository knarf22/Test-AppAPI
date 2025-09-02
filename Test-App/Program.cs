using Microsoft.EntityFrameworkCore;
using Test_App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register DbContext with connection string
builder.Services.AddDbContext<TestDbaseContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=test_dbase;User Id=franky;Password=password123;TrustServerCertificate=True;"));

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React dev server URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS before routing
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
