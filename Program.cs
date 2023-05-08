using Microsoft.EntityFrameworkCore;
using GarageLog.Models;
using Microsoft.Extensions.DependencyInjection;
using GarageLog.Data;
using Microsoft.AspNetCore.Builder;
using static System.Net.Mime.MediaTypeNames;

var CORSAllowSpecificOrigins = "_CORSAllowed";


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GarageLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GarageLogContext") ?? throw new InvalidOperationException("Connection string 'GarageLogContext' not found.")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSAllowSpecificOrigins,
    policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000");
    });
});


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

app.UseCors(CORSAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
