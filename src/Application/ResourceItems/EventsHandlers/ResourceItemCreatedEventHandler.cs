using RestTest.Domain.Events;

namespace RestTest.Application.ResourceItems.EventsHandlers;
public class ResourceItemCreatedEventHandler : INotificationHandler<ResourceItemCreatedEvent>
{
    public async Task Handle(ResourceItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
