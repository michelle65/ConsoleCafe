using ConsoleCafe.Domain.Events;

namespace ConsoleCafe.Application.Eventing
{
    public class SimpleOrderEventPublisher : IOrderEventPublisher
    {
        private readonly IEnumerable<IOrderEventSubscriber> _subscribers;

        public SimpleOrderEventPublisher(IEnumerable<IOrderEventSubscriber> subscribers)
        {
            _subscribers = subscribers;
        }

        public void Publish(OrderPlaced evt)
        {
            foreach (IOrderEventSubscriber subscriber in _subscribers)
            {
                subscriber.On(evt);
            }
        }
    }
}
