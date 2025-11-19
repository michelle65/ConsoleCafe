using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.Domain.Pricing
{
    public class HappyHourPricing : IPricingStrategy
    {
        public string Name => "Happy Hour";

        public decimal Apply(decimal subtotal) => decimal.Round(subtotal * 0.80m, 2);
    }
}
