using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebAPI.Tests
{
    public class SensorEventsControllerTests
    {
        private readonly Mock<ISensorEventService> _serviceMock = new();
        private readonly SensorEventsController _controller;

        public SensorEventsControllerTests()
        {
            _controller = new SensorEventsController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetSummary_ReturnsOkResult()
        {
            _serviceMock.Setup(s => s.GetSummaryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(new List<SensorEventSummary> { new SensorEventSummary { Gate = "Gate A", Type = "enter", TotalPeople = 100 } });

            var result = await _controller.GetSummary("Gate A", "enter", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var summaries = Assert.IsType<List<SensorEventSummary>>(okResult.Value);
            Assert.Equal("Gate A", summaries[0].Gate);
        }
    }
}