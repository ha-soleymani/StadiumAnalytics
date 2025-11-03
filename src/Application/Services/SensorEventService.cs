using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class SensorEventService : ISensorEventService

{
    private readonly ISensorEventRepository _repository;
    private readonly ILogger<SensorEventService> _logger;

    public SensorEventService(ISensorEventRepository repository, ILogger<SensorEventService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task AddEventAsync(SensorEvent sensorEvent)
    {
        _logger.LogInformation("Adding event: {@SensorEvent}", sensorEvent);
        try
        {
            await _repository.AddAsync(sensorEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add sensor event.");
            throw;
        }
    }

    public async Task<List<SensorEventSummary>> GetSummaryAsync(string? gate, string? type, DateTime? start, DateTime? end)
    {
        _logger.LogInformation("Fetching summary with filters: Gate={Gate}, Type={Type}, Start={Start}, End={End}", gate, type, start, end);

        try
        {
            var events = await _repository.GetSummaryAsync(gate, type, start, end);

            var summary = events
                .GroupBy(e => new { e.Gate, e.Type })
                .Select(g => new SensorEventSummary
                {
                    Gate = g.Key.Gate,
                    Type = g.Key.Type,
                    TotalPeople = g.Sum(e => e.NumberOfPeople)
                }).ToList();

            _logger.LogInformation("Summary result: {@Summary}", summary);
            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching summary.");
            throw;
        }
    }
}