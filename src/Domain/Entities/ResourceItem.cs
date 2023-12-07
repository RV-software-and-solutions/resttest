using RestTest.Domain.Common;

namespace RestTest.Domain.Entities;
public class ResourceItem : BaseAuditableEntity
{
    public required string Title { get; set; }

    public required string Location { get; set; }
}
