using RestTest.Domain.Common;
using RestTest.Domain.Entities;

namespace RestTest.Domain.Events;
public class ResourceItemCreatedEvent : BaseEvent
{
    public ResourceItem Item { get; }
    public ResourceItemCreatedEvent(ResourceItem item)
    {
        Item = item;
    }
}
