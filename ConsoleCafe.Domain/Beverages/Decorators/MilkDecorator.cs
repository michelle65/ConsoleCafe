using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages.Decorators
{
    public class MilkDecorator : BeverageDecorator
    {
        public MilkDecorator(IBeverage beverage) : base(beverage) { }

        public override decimal Cost() => _beverage.Cost() + 0.40m;

        public override string Describe() => $"{_beverage.Describe()}, milk";
    }
}
