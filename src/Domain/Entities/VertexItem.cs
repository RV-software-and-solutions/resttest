namespace RestTest.Domain.Entities;

/// <summary>
/// Represents a vertex item in a graph structure.
/// </summary>
public class VertexItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the vertex item.
    /// </summary>
    /// <value>The vertex item's unique identifier.</value>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the value associated with the vertex item.
    /// </summary>
    /// <value>The value stored in the vertex item.</value>
    public required string Value { get; set; }

    /// <summary>
    /// Gets or sets the list of IDs for adjacent vertex items.
    /// </summary>
    /// <value>The list of adjacent vertex item IDs. Can be null.</value>
    public required List<string>? AdjacentIds { get; set; }
}
