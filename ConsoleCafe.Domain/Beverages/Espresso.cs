using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages
{
    public class Espresso : IBeverage
    {
        public string Name => "Espresso";

        public decimal Cost() => 2.50m;

        public string Describe() => Name;
    }
}
