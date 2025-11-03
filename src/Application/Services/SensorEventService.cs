using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Logging;

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
        await _repository.AddAsync(sensorEvent);
    }

    public async Task<List<SensorEventSummary>> GetSummaryAsync(string gate, string type, DateTime? start, DateTime? end)
    {
        _logger.LogInformation("Fetching summary with filters: Gate={Gate}, Type={Type}, Start={Start}, End={End}", gate, type, start, end);
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
}