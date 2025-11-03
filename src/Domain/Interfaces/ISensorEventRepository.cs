public interface ISensorEventRepository
{
    Task AddAsync(SensorEvent sensorEvent);
    Task<List<SensorEvent>> GetSummaryAsync(string? gate, string? type, DateTime? start, DateTime? end);
}