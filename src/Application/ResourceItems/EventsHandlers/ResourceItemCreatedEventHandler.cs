using MediatR;
using RestTest.Domain.Events;

namespace RestTest.Application.ResourceItems.EventsHandler;
public class ResourceItemCreatedEventHandler : INotificationHandler<ResourceItemCreatedEvent>
{
    public async Task Handle(ResourceItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
