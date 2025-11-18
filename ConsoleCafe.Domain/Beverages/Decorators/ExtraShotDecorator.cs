using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages.Decorators
{
    public class ExtraShotDecorator : BeverageDecorator
    {
        public ExtraShotDecorator(IBeverage beverage) : base(beverage) { }

        public override decimal Cost() => _beverage.Cost() + 0.80m;

        public override string Describe() => $"{_beverage.Describe()}, extra shot";
    }
}
