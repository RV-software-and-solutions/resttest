namespace RestTest.Core.Services.Graph.Models;

/// <summary>
/// Represents a generic abstract vertex for use in graph data structures.
/// </summary>
/// <typeparam name="T">The type of the value stored in the vertex.</typeparam>
public abstract class AbstractVertex<T>
{
    /// <summary>
    /// Gets or sets the unique identifier of the vertex.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the value stored in the vertex.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Gets or sets the adjacent vertices to this vertex.
    /// </summary>
    public List<AbstractVertex<T>>? Adjacent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this vertex has been visited, typically used in traversal algorithms.
    /// </summary>
    public bool Visited { get; set; }

    /// <summary>
    /// Initializes a new vertex with a specified value. Generates a new unique identifier for the vertex.
    /// </summary>
    /// <param name="value">The value to store in the vertex.</param>
    /// <returns>The initialized vertex with a unique Id and the specified value.</returns>
    public virtual AbstractVertex<T> InitVertex(T value)
    {
        Id = Guid.NewGuid().ToString();
        Value = value;
        Adjacent = new List<AbstractVertex<T>>();
        Visited = false;
        return this;
    }

    /// <summary>
    /// Loads a vertex with a specified id and value. Used for constructing a vertex with a known identifier.
    /// </summary>
    /// <param name="id">The identifier of the vertex.</param>
    /// <param name="value">The value to store in the vertex.</param>
    /// <returns>The vertex with the specified id and value.</returns>
    public virtual AbstractVertex<T> LoadVertex(string id, T value)
    {
        Id = id;
        Value = value;
        Adjacent = new List<AbstractVertex<T>>();
        Visited = false;
        return this;
    }

    /// <summary>
    /// Adds an edge from this vertex to another vertex.
    /// </summary>
    /// <param name="vertex">The vertex to connect to this vertex.</param>
    public virtual void AddEdge(AbstractVertex<T> vertex)
    {
        Adjacent?.Add(vertex);
    }
}
