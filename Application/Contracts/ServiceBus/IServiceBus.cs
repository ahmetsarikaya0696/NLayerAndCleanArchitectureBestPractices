using Domain.Events;

namespace Application.Contracts.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IEventOrMessage;
        Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage;
    }
}
