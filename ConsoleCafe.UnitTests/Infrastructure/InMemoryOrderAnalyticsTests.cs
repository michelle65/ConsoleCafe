using ConsoleCafe.Domain.Events;
using ConsoleCafe.Infrastructure.Analytics;

namespace ConsoleCafe.UnitTests.Infrastructure
{
    public class InMemoryOrderAnalyticsTests
    {
        [Fact]
        public void On_ShouldAccumulateOrdersAndRevenue()
        {
            var analytics = new InMemoryOrderAnalytics();
            var firstOrderEvent = new OrderPlaced(Guid.NewGuid(), DateTimeOffset.Now, "Coffee", 3.00m, 3.00m);
            var secondOrderEvent = new OrderPlaced(Guid.NewGuid(), DateTimeOffset.Now, "Tea", 2.00m, 1.60m);

            analytics.On(firstOrderEvent);
            analytics.On(secondOrderEvent);

            Assert.Equal(2, analytics.OrderCount);
            Assert.Equal(4.60m, analytics.TotalRevenue);
        }

        [Fact]
        public void Reset_ShouldClearState()
        {
            var analytics = new InMemoryOrderAnalytics();
            var orderEvent = new OrderPlaced(Guid.NewGuid(), DateTimeOffset.Now, "Coffee", 3.00m, 3.00m);

            analytics.On(orderEvent);

            analytics.Reset();

            Assert.Equal(0, analytics.OrderCount);
            Assert.Equal(0m, analytics.TotalRevenue);
        }
    }
}
