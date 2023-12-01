using RestTest.Core.Services.Graph.Models;

namespace RestTest.Core.Services.Graph;
public class GraphService<TVertex, TVertexValue> : IGraphService<TVertex, TVertexValue> where TVertex : AbstractVertex<TVertexValue>, new()
{
    public List<TVertex> Vertices { get; set; }

    public GraphService()
    {
        Vertices = new List<TVertex>();
    }

    public void AddVertex(TVertex vertex)
    {
        Vertices.Add(vertex);
    }

    public void AddEdge(TVertex from, TVertex to)
    {
        from.AddEdge(to);
    }

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

    public bool DoesEdgeExist(TVertex v1, TVertex v2)
    {
        return v1.Adjacent.Contains(v2) || v2.Adjacent.Contains(v1);
    }

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

    public void IfNotExistsAddEdge(TVertex v1, TVertex v2)
    {
        if (!DoesEdgeExist(v1, v2))
        {
            AddEdge(v1, v2);
        }
    }

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
