using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Beverages.Decorators
{
    public abstract class BeverageDecorator : IBeverage
    {
        protected readonly IBeverage _beverage;

        protected BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public virtual string Name => _beverage.Name;

        public abstract decimal Cost();

        public abstract string Describe();
    }
}
