using ConsoleCafe.Application.Eventing;
using ConsoleCafe.Domain.Events;
using Moq;

namespace ConsoleCafe.UnitTests.Application
{
    public class SimpleOrderEventPublisherTests
    {
        [Fact]
        public void Publish_ShouldNotifyAllSubscribers()
        {
            var subscriber1 = new Mock<IOrderEventSubscriber>();
            var subscriber2 = new Mock<IOrderEventSubscriber>();
            var publisher = new SimpleOrderEventPublisher([
                subscriber1.Object,
                subscriber2.Object
            ]);

            var orderPlacedEvent = new OrderPlaced(Guid.NewGuid(), DateTimeOffset.Now, "Test", 1m, 1.2m);

            publisher.Publish(orderPlacedEvent);

            subscriber1.Verify(s => s.On(orderPlacedEvent), Times.Once);
            subscriber2.Verify(s => s.On(orderPlacedEvent), Times.Once);
        }
    }
}