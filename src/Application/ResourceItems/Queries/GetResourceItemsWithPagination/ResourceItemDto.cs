using RestTest.Application.Common.Mappings;
using RestTest.Domain.Entities;

namespace RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;
public class ResourceItemDto : IMapFrom<ResourceItem>
{
    public required string Title { get; set; }

    public required string Location { get; set; }
}
