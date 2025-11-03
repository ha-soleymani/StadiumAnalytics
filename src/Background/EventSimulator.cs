using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Domain.Entities;

namespace Background;
public class EventSimulator : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EventSimulator> _logger;
    private readonly Random _random = new();

    public EventSimulator(IServiceScopeFactory scopeFactory, ILogger<EventSimulator> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EventSimulator started.");  
        var rand = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ISensorEventService>();

                var evt = new SensorEvent
                {
                    Gate = $"Gate {Convert.ToChar('A' + _random.Next(0, 5))}",
                    Timestamp = DateTime.UtcNow,
                    NumberOfPeople = _random.Next(1, 20),
                    Type = _random.Next(0, 2) == 0 ? "enter" : "leave"
                };

                await service.AddEventAsync(evt);
                _logger.LogInformation("Simulated event: {@SensorEvent}", evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during event simulation.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        _logger.LogInformation("EventSimulator stopped.");
    }
}