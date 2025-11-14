using ConsoleCafe.Domain.Pricing;
using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class OrderRequest
    {
        public string BeverageType { get; set; } = string.Empty;

        public List<string> AddOns { get; set; } = new();

        public List<string> AddOnFlavors { get; set; } = new();

        public IPricingStrategy PricingStrategy { get; set; } = new RegularPricing();
    }
}