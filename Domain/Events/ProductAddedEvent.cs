namespace Domain.Events
{
    public record ProductAddedEvent(int Id, string Name, decimal Price) : IEventOrMessage;
}
