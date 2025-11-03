using Domain.Entities;

namespace Domain.Interfaces;

// Interface for sensor event repository
public interface ISensorEventRepository
{
    // Adds a new sensor event to the repository
    Task AddAsync(SensorEvent sensorEvent);

    // Retrieves sensor events based on optional filters
    Task<List<SensorEvent>> GetSummaryAsync(string? gate, string? type, DateTime? start, DateTime? end);
}