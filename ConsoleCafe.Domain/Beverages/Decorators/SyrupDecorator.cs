using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages.Decorators
{
    public class SyrupDecorator : BeverageDecorator
    {
        private readonly string _flavor;

        public SyrupDecorator(IBeverage beverage, string flavor) : base(beverage)
        {
            _flavor = flavor;
        }

        public override decimal Cost() => _beverage.Cost() + 0.50m;

        public override string Describe() => $"{_beverage.Describe()}, {_flavor} syrup";
    }
}
