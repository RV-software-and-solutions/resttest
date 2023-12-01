using RestTest.Domain.Common;

namespace RestTest.Domain.Entities;
public class VertexItem : BaseEntity
{
    public required string Value { get; set; }
    public required List<string> AdjacentIds { get; set; }
}
