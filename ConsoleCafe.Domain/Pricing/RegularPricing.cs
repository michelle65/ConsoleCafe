using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.Domain.Pricing
{
    public class RegularPricing : IPricingStrategy
    {
        public string Name => "Regular";

        public decimal Apply(decimal subtotal) => subtotal;
    }
}
