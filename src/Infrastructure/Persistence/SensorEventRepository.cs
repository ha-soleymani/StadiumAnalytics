using Microsoft.EntityFrameworkCore;

public class SensorEventRepository : ISensorEventRepository
{
    private readonly AnalyticsDbContext _context;

    public SensorEventRepository(AnalyticsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SensorEvent sensorEvent)
    {
        _context.SensorEvents.Add(sensorEvent);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SensorEvent>> GetSummaryAsync(string? gate, string? type, DateTime? start, DateTime? end)
    {
        var query = _context.SensorEvents.AsQueryable();

        if (!string.IsNullOrEmpty(gate)) query = query.Where(e => e.Gate == gate);
        if (!string.IsNullOrEmpty(type)) query = query.Where(e => e.Type == type);
        if (start.HasValue) query = query.Where(e => e.Timestamp >= start.Value);
        if (end.HasValue) query = query.Where(e => e.Timestamp <= end.Value);

        return await query.ToListAsync();
    }
}