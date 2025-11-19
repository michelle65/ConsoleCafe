using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages
{
    public class Tea : IBeverage
    {
        public string Name => "Tea";

        public decimal Cost() => 2.00m;

        public string Describe() => Name;
    }
}
