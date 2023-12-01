using RestTest.Core.Services.Graph.Models;

namespace RestTest.Core.Services.Graph;

public interface IGraphService<TVertex, TVertexValue> where TVertex : AbstractVertex<TVertexValue>
{
    List<TVertex> Vertices { get; set; }

    void AddVertex(TVertex vertex);

    void AddEdge(TVertex from, TVertex to);

    bool DoesEdgeExist(TVertex v1, TVertex v2);

    TVertex? Search(TVertexValue value);

    void AddEdgeToFoundVertex(TVertexValue existingValue, TVertexValue newValue);

    TVertex? DFS(TVertex vertex, TVertexValue value);

    TVertex IfNotExistsAddVertex(TVertexValue vertexValue);

    void IfNotExistsAddEdge(TVertex v1, TVertex v2);

    List<TVertex> GetAllConnectedVertices(TVertex startVertex);
}
