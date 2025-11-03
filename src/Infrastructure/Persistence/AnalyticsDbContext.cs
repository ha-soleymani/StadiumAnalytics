using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class AnalyticsDbContext : DbContext
{
    public DbSet<SensorEvent> SensorEvents { get; set; }

    public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SensorEvent>().HasKey(e => e.Id);
    }
}