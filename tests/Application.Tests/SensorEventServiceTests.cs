using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests
{
    public class SensorEventServiceTests
    {
        private readonly Mock<ISensorEventRepository> _mockRepo = new();
        private readonly Mock<ILogger<SensorEventService>> _mockLogger = new();
        private readonly ISensorEventService _service;

        public SensorEventServiceTests()
        {
            _service = new SensorEventService(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetSummaryAsync_ReturnsCorrectAggregation()
        {
            _mockRepo.Setup(r => r.GetSummaryAsync(null, null, null, null)).ReturnsAsync(new List<SensorEvent>
            {
                new SensorEvent { Gate = "Gate A", Type = "enter", NumberOfPeople = 10 },
                new SensorEvent { Gate = "Gate A", Type = "enter", NumberOfPeople = 20 },
                new SensorEvent { Gate = "Gate B", Type = "leave", NumberOfPeople = 5 }
            });

            var result = await _service.GetSummaryAsync(null, null, null, null);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(30, result.First(r => r.Gate == "Gate A" && r.Type == "enter").TotalPeople);
            Assert.Equal(5, result.First(r => r.Gate == "Gate B" && r.Type == "leave").TotalPeople);
        }

        [Fact]
        public async Task AddEventAsync_ValidEvent_CallsRepository()
        {
            var evt = new SensorEvent { Gate = "Gate A", Timestamp = DateTime.UtcNow, NumberOfPeople = 5, Type = "enter" };

            await _service.AddEventAsync(evt);

            _mockRepo.Verify(r => r.AddAsync(It.Is<SensorEvent>(e => e.Gate == "Gate A")), Times.Once);
        }
    }
}