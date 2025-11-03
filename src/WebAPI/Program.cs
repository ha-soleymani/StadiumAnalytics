using Application.Interfaces;
using Application.Services;
using Background;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAPI.HealthCheck;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AnalyticsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISensorEventRepository, SensorEventRepository>();
builder.Services.AddScoped<ISensorEventService, SensorEventService>();
builder.Services.AddHostedService<EventSimulator>();
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
    .AddCheck<DbContextHealthCheck>("Database");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stadium Analytics API v1");
        c.RoutePrefix = "swagger"; // or "" if you want it at root
    });
}

app.Services.CreateScope().ServiceProvider
    .GetRequiredService<AnalyticsDbContext>()
    .Database.EnsureCreated();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
