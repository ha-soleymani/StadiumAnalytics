using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory; // Add this using directive

namespace Infrastructure.Tests
{
    public class SensorEventRepositoryTests
    {
        private readonly DbContextOptions<AnalyticsDbContext> _options;

        public SensorEventRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AnalyticsDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_PersistsEvent()
        {
            using var context = new AnalyticsDbContext(_options);
            var repo = new SensorEventRepository(context);

            var evt = new SensorEvent { Gate = "Gate B", Timestamp = DateTime.UtcNow, NumberOfPeople = 10, Type = "leave" };
            await repo.AddAsync(evt);

            var saved = await context.SensorEvents.FirstOrDefaultAsync();
            Assert.Equal("Gate B", saved?.Gate);
        }
    }
}