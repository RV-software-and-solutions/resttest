using RestTest.Core.Services.Graph.Models;

namespace RestTest.Core.Services.Graph;

/// <summary>
/// Provides services for managing a graph data structure.
/// </summary>
/// <typeparam name="TVertex">The type of the vertices in the graph.</typeparam>
/// <typeparam name="TVertexValue">The type of the values stored in the vertices.</typeparam>
public class GraphService<TVertex, TVertexValue> : IGraphService<TVertex, TVertexValue> where TVertex : AbstractVertex<TVertexValue>, new()
{
    /// <inheritdoc/>
    public List<TVertex> Vertices { get; set; }

    /// <summary>
    /// Initializes a new instance of the GraphService class.
    /// </summary>
    public GraphService()
    {
        Vertices = new List<TVertex>();
    }

    /// <inheritdoc/>
    public void AddVertex(TVertex vertex)
    {
        Vertices.Add(vertex);
    }

    /// <inheritdoc/>
    public void AddEdge(TVertex from, TVertex to)
    {
        from.AddEdge(to);
    }

    /// <inheritdoc/>
    public TVertex? Search(TVertexValue value)
    {
        foreach (var vertex in Vertices)
        {
            vertex.Visited = false;
        }

        foreach (var vertex in Vertices)
        {
            if (!vertex.Visited)
            {
                var foundVertex = DFS(vertex, value);
                if (foundVertex != null)
                {
                    return foundVertex;
                }
            }
        }

        return default;
    }

    /// <inheritdoc/>
    public void AddEdgeToFoundVertex(TVertexValue existingValue, TVertexValue newValue)
    {
        TVertex newVertex = new();
        newVertex.InitVertex(newValue);

        AddVertex(newVertex);
        TVertex? foundVertex = Search(existingValue);
        if (foundVertex != null)
        {
            AddEdge(foundVertex, newVertex);
        }
    }

    /// <inheritdoc/>
    public TVertex? DFS(TVertex vertex, TVertexValue value)
    {
        if (vertex.Visited)
        {
            return null;
        }

        vertex.Visited = true;

        if (EqualityComparer<TVertexValue>.Default.Equals(vertex.Value, value))
        {
            return vertex;
        }

        foreach (TVertex adj in vertex.Adjacent.OfType<TVertex>())
        {
            TVertex? foundVertex = DFS(adj, value);
            if (foundVertex != null)
            {
                return foundVertex;
            }
        }

        return default;
    }

    /// <inheritdoc/>
    public bool DoesEdgeExist(TVertex v1, TVertex v2)
    {
        return v1.Adjacent.Contains(v2) || v2.Adjacent.Contains(v1);
    }

    /// <inheritdoc/>
    public TVertex IfNotExistsAddVertex(TVertexValue vertexValue)
    {
        TVertex? vertex = Search(vertexValue);
        if (vertex == null)
        {
            vertex = new();
            vertex.InitVertex(vertexValue);
            AddVertex(vertex);
        }

        return vertex;
    }

    /// <inheritdoc/>
    public void IfNotExistsAddEdge(TVertex v1, TVertex v2)
    {
        if (!DoesEdgeExist(v1, v2))
        {
            AddEdge(v1, v2);
            AddEdge(v2, v1);
        }
    }

    /// <inheritdoc/>
    public List<TVertex> GetAllConnectedVertices(TVertex startVertex)
    {
        foreach (TVertex vertex in Vertices)
        {
            vertex.Visited = false;
        }

        List<TVertex> allVertices = new();

        RecursiveDFS(startVertex, allVertices);

        return allVertices;
    }

    /// <inheritdoc/>
    public List<TVertex> GetAllVertices()
    {
        List<TVertex> visitedOrder = new();

        foreach (var vertex in Vertices)
        {
            vertex.Visited = false;
        }

        if (Vertices.Count > 0)
        {
            RecursiveDFS(Vertices[0], visitedOrder);
        }

        foreach (TVertex? vertex in Vertices.Where(v => !v.Visited))
        {
            RecursiveDFS(vertex, visitedOrder);
        }

        return visitedOrder;
    }

    /// <inheritdoc/>
    public void DeleteVertex(TVertex vertexToDelete)
    {
        Vertices.Remove(vertexToDelete);

        foreach (TVertex vertex in Vertices)
        {
            vertex.Adjacent.RemoveAll(adj => adj == vertexToDelete);
        }
    }

    /// <inheritdoc/>
    public void LoadListIntoGraph(List<TVertex> vertices)
    {
        Vertices.Clear();

        Dictionary<string, TVertex> verticesById = new();

        foreach (TVertex item in vertices)
        {
            TVertex vertex = new();
            vertex.LoadVertex(item.Id, item.Value);
            verticesById.Add(item.Id, vertex);
            Vertices.Add(vertex);
        }

        foreach (var item in vertices)
        {
            TVertex vertex = verticesById[item.Id];
            if (item.Adjacent is not null)
            {
                foreach (AbstractVertex<TVertexValue> adjacent in item.Adjacent)
                {
                    if (verticesById.TryGetValue(adjacent.Id, out var adjacentVertex))
                    {
                        vertex.AddEdge(adjacentVertex);
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    public void ClearGraph()
    {
        Vertices.Clear();
    }

    /// <summary>
    /// Recursive depth-first search to collect all connected vertices.
    /// </summary>
    /// <param name="vertex">The starting vertex.</param>
    /// <param name="allVertices">A list to store the visited vertices.</param>
    private void RecursiveDFS(TVertex vertex, List<TVertex> allVertices)
    {
        vertex.Visited = true;
        allVertices.Add(vertex);
        foreach (var adj in vertex.Adjacent.OfType<TVertex>().Where(adj => !adj.Visited))
        {
            RecursiveDFS(adj, allVertices);
        }
    }
}
