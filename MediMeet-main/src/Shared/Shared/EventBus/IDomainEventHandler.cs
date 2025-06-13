using MediatR;

namespace Shared.EventBus;

public interface IDomainEventHandler<in TEvent>:INotificationHandler<TEvent> where TEvent:IDomainEvent
{
    
}