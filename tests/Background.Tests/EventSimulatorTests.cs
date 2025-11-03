using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Background.Tests
{
    public class EventSimulatorTests
    {
        [Fact]
        public async Task EventSimulator_GeneratesEvents()
        {
            var service = new Mock<ISensorEventService>();
            var scopeFactory = new Mock<IServiceScopeFactory>();
            var scope = new Mock<IServiceScope>();
            var provider = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<EventSimulator>>();

            provider.Setup(p => p.GetService(typeof(ISensorEventService))).Returns(service.Object);
            scope.Setup(s => s.ServiceProvider).Returns(provider.Object);
            scopeFactory.Setup(f => f.CreateScope()).Returns(scope.Object);

            var simulator = new EventSimulator(scopeFactory.Object, logger.Object);

            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            await simulator.StartAsync(tokenSource.Token);

            service.Verify(s => s.AddEventAsync(It.IsAny<SensorEvent>()), Times.AtLeastOnce);
        }
    }
}