using Domain.Events;
using MassTransit;

namespace Bus.Consumers
{
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Gelen event : Id:{context.Message.Id} Name:{context.Message.Name} Price:{context.Message.Price}");
            return Task.CompletedTask;
        }
    }
}
