using Microsoft.EntityFrameworkCore;
using OnlineLibrarySystem.Mapper;
using OnlineLibrarySystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// establish connection db 
var connectionString = builder.Configuration.GetConnectionString("LibraryDB");
builder.Services.AddDbContext<LibraryDbCotext>(options => options.UseSqlServer(connectionString));

// auto mapper register
builder.Services.AddAutoMapper(typeof(Mappers));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
