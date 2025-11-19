namespace ConsoleCafe.Domain.Pricing.Interfaces
{
    public interface IPricingStrategy
    {
        decimal Apply(decimal subtotal);
        string Name { get; }
    }
}
