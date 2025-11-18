using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Pricing;
using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class OrderInputCollector
    {
        private readonly MenuRenderer _renderer;
        private readonly MenuInputReader _reader;

        public OrderInputCollector(MenuRenderer renderer, MenuInputReader reader)
        {
            _renderer = renderer;
            _reader = reader;
        }

        public OrderRequest Collect()
        {
            _renderer.RenderHeader();
            _renderer.RenderBeverageOptions();

            var beverageChoice = _reader.ReadOption("Enter your choice (1-3): ", new[] { "1", "2", "3" });
            var beverageType = beverageChoice switch
            {
                "1" => BeverageType.Espresso,
                "2" => BeverageType.Tea,
                "3" => BeverageType.HotChocolate,
                _ => BeverageType.Espresso
            };

            var addOns = new List<AddOnType>();
            var addOnFlavors = new List<string>();

            var addingAddOns = true;
            while (addingAddOns)
            {
                _renderer.RenderAddOnOptions();
                var addOnChoice = _reader.ReadOption("Enter your choice (0-3): ", ["0", "1", "2", "3"]);

                switch (addOnChoice)
                {
                    case "0":
                        addingAddOns = false;
                        break;
                    case "1":
                        addOns.Add(AddOnType.Milk);
                        addOnFlavors.Add(string.Empty);
                        break;
                    case "2":
                        addOns.Add(AddOnType.Syrup);
                        var flavor = _reader.ReadText("Enter syrup flavor (e.g., vanilla): ", "vanilla");
                        addOnFlavors.Add(flavor);
                        break;
                    case "3":
                        addOns.Add(AddOnType.ExtraShot);
                        addOnFlavors.Add(string.Empty);
                        break;
                }
            }

            _renderer.RenderPricingOptions();
            var pricingChoice = _reader.ReadOption("Enter your choice (1-2): ", new[] { "1", "2" });
            IPricingStrategy pricingStrategy = pricingChoice is "2"
                ? new HappyHourPricing()
                : new RegularPricing();

            return new OrderRequest
            {
                BeverageType = beverageType,
                AddOns = addOns,
                AddOnFlavors = addOnFlavors,
                PricingStrategy = pricingStrategy
            };
        }
    }
}