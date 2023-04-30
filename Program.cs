using Microsoft.EntityFrameworkCore;
using GarageLog.Models;
using Microsoft.Extensions.DependencyInjection;
using GarageLog.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GarageLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GarageLogContext") ?? throw new InvalidOperationException("Connection string 'GarageLogContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
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
