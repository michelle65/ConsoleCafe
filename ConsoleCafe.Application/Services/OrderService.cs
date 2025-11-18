using ConsoleCafe.Application.Dtos;
using ConsoleCafe.Domain.Beverages.Decorators;
using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Events;
using ConsoleCafe.Domain.Factories.Interfaces;
using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.Application.Services
{
    public class OrderService
    {
        private readonly IBeverageFactory _beverageFactory;
        private readonly IOrderEventPublisher _eventPublisher;

        public OrderService(IBeverageFactory beverageFactory, IOrderEventPublisher eventPublisher)
        {
            _beverageFactory = beverageFactory;
            _eventPublisher = eventPublisher;
        }

        public OrderResultDto ProcessOrder(
            BeverageType beverageType,
            AddOnType[] addOns,
            string[] addOnFlavors,
            IPricingStrategy pricingStrategy)
        {
            var beverage = _beverageFactory.Create(beverageType);

            for (int i = 0; i < addOns.Length; i++)
            {
                var addOn = addOns[i];
                beverage = addOn switch
                {
                    AddOnType.Milk => new MilkDecorator(beverage),
                    AddOnType.Syrup => new SyrupDecorator(beverage, addOnFlavors[i]),
                    AddOnType.ExtraShot => new ExtraShotDecorator(beverage),
                    _ => beverage
                };
            }

            var subtotal = beverage.Cost();
            var total = pricingStrategy.Apply(subtotal);
            var description = beverage.Describe();
            var orderId = Guid.NewGuid();
            var timestamp = DateTimeOffset.Now;

            var orderEvent = new OrderPlaced(
                orderId,
                timestamp,
                description,
                subtotal,
                total);

            _eventPublisher.Publish(orderEvent);

            return new OrderResultDto
            {
                OrderId = orderId,
                Timestamp = timestamp,
                Description = description,
                Subtotal = subtotal,
                Total = total
            };
        }
    }
}
