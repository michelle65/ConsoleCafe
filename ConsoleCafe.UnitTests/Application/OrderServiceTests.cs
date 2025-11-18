using ConsoleCafe.Application.Services;
using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Beverages.Interfaces;
using ConsoleCafe.Domain.Events;
using ConsoleCafe.Domain.Factories.Interfaces;
using ConsoleCafe.Domain.Pricing;
using Moq;

namespace ConsoleCafe.UnitTests.Application
{
    public class OrderServiceTests
    {
        private readonly Mock<IBeverageFactory> _beverageFactoryMock = new();
        private readonly Mock<IOrderEventPublisher> _eventPublisherMock = new();

        [Fact]
        public void ProcessOrder_ShouldReturnDecoratedBeverageAndPublishEvent()
        {
            var baseBeverageMock = new Mock<IBeverage>();
            baseBeverageMock.Setup(b => b.Cost()).Returns(2.50m);
            baseBeverageMock.Setup(b => b.Describe()).Returns("Espresso");
            baseBeverageMock.SetupGet(b => b.Name).Returns("Espresso");

            _beverageFactoryMock
                .Setup(factory => factory.Create(BeverageType.Espresso))
                .Returns(baseBeverageMock.Object);

            OrderPlaced? publishedEvent = null;
            _eventPublisherMock
                .Setup(publisher => publisher.Publish(It.IsAny<OrderPlaced>()))
                .Callback<OrderPlaced>(evt => publishedEvent = evt);

            var service = new OrderService(_beverageFactoryMock.Object, _eventPublisherMock.Object);

            var result = service.ProcessOrder(
               BeverageType.Espresso,
               [AddOnType.Milk, AddOnType.Syrup],
                [string.Empty, "vanilla"],
                PricingStrategyType.Regular);

            Assert.Equal("Espresso, milk, vanilla syrup", result.Description);
            Assert.Equal(3.40m, result.Subtotal);
            Assert.Equal(result.Subtotal, result.Total);
            Assert.NotEqual(Guid.Empty, result.OrderId);
            Assert.True(result.Timestamp > DateTimeOffset.MinValue);

            _eventPublisherMock.Verify(publisher => publisher.Publish(It.IsAny<OrderPlaced>()), Times.Once);

            Assert.NotNull(publishedEvent);
            if (publishedEvent is not null)
            {
                Assert.Equal(result.OrderId, publishedEvent.OrderId);
                Assert.Equal(result.Timestamp, publishedEvent.At);
                Assert.Equal(result.Description, publishedEvent.Description);
                Assert.Equal(result.Total, publishedEvent.Total);
            }

            _beverageFactoryMock.Verify(factory => factory.Create(BeverageType.Espresso), Times.Once);
        }
    }
}
