using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public class SensorEventRepository : ISensorEventRepository
{
    private readonly AnalyticsDbContext _context;

    public SensorEventRepository(AnalyticsDbContext context)
    {
        _context = context;
    }

    // Adds a new sensor event to the database
    public async Task AddAsync(SensorEvent sensorEvent)
    {
        _context.SensorEvents.Add(sensorEvent);
        await _context.SaveChangesAsync();
    }

    // Retrieves sensor events based on optional filters
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