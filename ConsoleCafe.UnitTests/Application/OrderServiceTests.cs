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
        private readonly Mock<IOrderEventPublisher> _orderEventPublisherMock = new();

        private readonly OrderService _orderService;
        private OrderPlaced? _capturedOrderPlacedEvent;

        public OrderServiceTests()
        {
            var espressoBeverageMock = new Mock<IBeverage>();
            espressoBeverageMock.Setup(b => b.Cost()).Returns(2.50m);
            espressoBeverageMock.Setup(b => b.Describe()).Returns("Espresso");
            espressoBeverageMock.SetupGet(b => b.Name).Returns("Espresso");

            _beverageFactoryMock
                .Setup(factory => factory.Create(BeverageType.Espresso))
                .Returns(espressoBeverageMock.Object);

            _orderEventPublisherMock
                .Setup(publisher => publisher.Publish(It.IsAny<OrderPlaced>()))
                .Callback<OrderPlaced>(published => _capturedOrderPlacedEvent = published);

            _orderService = new OrderService(_beverageFactoryMock.Object, _orderEventPublisherMock.Object);
        }

        [Fact]
        public void ProcessOrder_ShouldReturnDecoratedBeverageResult()
        {
            var addOnTypes = new[] { AddOnType.Milk, AddOnType.Syrup };
            var addOnOptions = new[] { string.Empty, "vanilla" };

            var actual = _orderService.ProcessOrder(BeverageType.Espresso, addOnTypes, addOnOptions, PricingStrategyType.Regular);

            Assert.Equal("Espresso, milk, vanilla syrup", actual.Description);
            Assert.Equal(3.40m, actual.Subtotal);
            Assert.Equal(actual.Subtotal, actual.Total);
            Assert.NotEqual(Guid.Empty, actual.OrderId);
            Assert.True(actual.Timestamp > DateTimeOffset.MinValue);

            _beverageFactoryMock.Verify(factory => factory.Create(BeverageType.Espresso), Times.Once);
        }

        [Fact]
        public void ProcessOrder_ShouldPublishOrderPlacedEventOnce_WithExpectedPayload()
        {
            var addOnTypes = new[] { AddOnType.Milk, AddOnType.Syrup };
            var addOnOptions = new[] { string.Empty, "vanilla" };

            var actual = _orderService.ProcessOrder(BeverageType.Espresso, addOnTypes, addOnOptions, PricingStrategyType.Regular);

            _orderEventPublisherMock.Verify(publisher => publisher.Publish(It.IsAny<OrderPlaced>()), Times.Once);

            Assert.NotNull(_capturedOrderPlacedEvent);
            var published = _capturedOrderPlacedEvent!;
            Assert.Equal(actual.OrderId, published.OrderId);
            Assert.Equal(actual.Timestamp, published.At);
            Assert.Equal(actual.Description, published.Description);
            Assert.Equal(actual.Total, published.Total);
        }
    }
}
