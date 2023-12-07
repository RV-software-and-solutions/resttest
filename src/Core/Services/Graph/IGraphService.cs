using RestTest.Core.Services.Graph.Models;

namespace RestTest.Core.Services.Graph;

/// <summary>
/// Defines the contract for a service that manages a graph data structure.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices in the graph.</typeparam>
/// <typeparam name="TVertexValue">The type of the values stored in the vertices.</typeparam>
public interface IGraphService<TVertex, TVertexValue> where TVertex : AbstractVertex<TVertexValue>
{
    /// <summary>
    /// Gets or sets the list of vertices in the graph.
    /// </summary>
    List<TVertex> Vertices { get; set; }

    /// <summary>
    /// Adds a vertex to the graph.
    /// </summary>
    /// <param name="vertex">The vertex to add.</param>
    void AddVertex(TVertex vertex);

    /// <summary>
    /// Adds an edge between two vertices in the graph.
    /// </summary>
    /// <param name="from">The source vertex.</param>
    /// <param name="to">The destination vertex.</param>
    void AddEdge(TVertex from, TVertex to);

    /// <summary>
    /// Checks if an edge exists between two vertices.
    /// </summary>
    /// <param name="v1">The first vertex.</param>
    /// <param name="v2">The second vertex.</param>
    /// <returns>True if an edge exists, otherwise false.</returns>
    bool DoesEdgeExist(TVertex v1, TVertex v2);

    /// <summary>
    /// Searches for a vertex with a given value.
    /// </summary>
    /// <param name="value">The value to search for in the graph.</param>
    /// <returns>The vertex with the specified value, or null if not found.</returns>
    TVertex? Search(TVertexValue value);

    /// <summary>
    /// Adds an edge to a vertex found by its value, creating a new vertex if necessary.
    /// </summary>
    /// <param name="existingValue">The value of the existing vertex.</param>
    /// <param name="newValue">The value of the new vertex to connect.</param>
    void AddEdgeToFoundVertex(TVertexValue existingValue, TVertexValue newValue);

    /// <summary>
    /// Depth-first search algorithm to find a vertex with a given value.
    /// </summary>
    /// <param name="vertex">The starting vertex for the search.</param>
    /// <param name="value">The value to search for.</param>
    /// <returns>The found vertex, or null if not found.</returns>
    TVertex? DFS(TVertex vertex, TVertexValue value);

    /// <summary>
    /// Adds a vertex if it does not already exist in the graph.
    /// </summary>
    /// <param name="vertexValue">The value of the vertex to add or find.</param>
    /// <returns>The existing or new vertex.</returns>
    TVertex IfNotExistsAddVertex(TVertexValue vertexValue);

    /// <summary>
    /// Adds an edge between two vertices if it does not already exist.
    /// </summary>
    /// <param name="v1">The first vertex.</param>
    /// <param name="v2">The second vertex.</param>
    void IfNotExistsAddEdge(TVertex v1, TVertex v2);

    /// <summary>
    /// Retrieves all vertices connected to a given start vertex.
    /// </summary>
    /// <param name="startVertex">The vertex from which to start the search.</param>
    /// <returns>A list of all connected vertices.</returns>
    List<TVertex> GetAllConnectedVertices(TVertex startVertex);

    /// <summary>
    /// Retrieves all vertices in the graph.
    /// </summary>
    /// <returns>A list of all vertices.</returns>
    List<TVertex> GetAllVertices();

    /// <summary>
    /// Deletes a specified vertex from the graph.
    /// </summary>
    /// <param name="vertexToDelete">The vertex to delete.</param>
    void DeleteVertex(TVertex vertexToDelete);

    /// <summary>
    /// Loads a list of vertices into the graph.
    /// </summary>
    /// <param name="vertices">The list of vertices to load into the graph.</param>
    void LoadListIntoGraph(List<TVertex> vertices);

    /// <summary>
    /// Clears all vertices from the graph.
    /// </summary>
    void ClearGraph();
}
