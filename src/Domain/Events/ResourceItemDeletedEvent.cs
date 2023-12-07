using RestTest.Domain.Common;
using RestTest.Domain.Entities;

namespace RestTest.Domain.Events;
public class ResourceItemDeletedEvent : BaseEvent
{
    public ResourceItem Item { get; }
    public ResourceItemDeletedEvent(ResourceItem item)
    {
        Item = item;
    }
}
