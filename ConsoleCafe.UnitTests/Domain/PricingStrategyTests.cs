using ConsoleCafe.Domain.Pricing;

namespace ConsoleCafe.UnitTests.Domain
{
    public class PricingStrategyTests
    {
        [Fact]
        public void RegularPricing_ShouldReturnSubtotal()
        {
            var pricing = new RegularPricing();

            var total = pricing.Apply(10.00m);

            Assert.Equal(10.00m, total);
            Assert.Equal("Regular", pricing.Name);
        }

        [Fact]
        public void HappyHourPricing_ShouldApplyDiscount()
        {
            var pricing = new HappyHourPricing();

            var total = pricing.Apply(10.00m);

            Assert.Equal(8.00m, total);
            Assert.Equal("Happy Hour", pricing.Name);
        }
    }
}
