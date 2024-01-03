using RestTest.Core.Services.Graph;
using RestTest.Domain.Dtos.Synonym;

namespace RestTest.Application.Services;

/// <summary>
/// Provides services for managing synonyms using a graph data structure.
/// </summary>
public class SynonymService : ISynonymService
{
    /// <summary>
    /// The graph service to handle operations on the graph of synonyms.
    /// </summary>
    public readonly IGraphService<SynonymVertex, string> _graphService;

    /// <summary>
    /// Initializes a new instance of the SynonymService class.
    /// </summary>
    /// <param name="graphService">The graph service to be used for synonym operations.</param>
    public SynonymService(IGraphService<SynonymVertex, string> graphService)
    {
        _graphService = graphService;
    }

    /// <inheritdoc/>
    public void AddSynonym(string word, string synonymOfWord)
    {
        SynonymVertex wordVertex = _graphService.IfNotExistsAddVertex(word);
        SynonymVertex synonymVertex = _graphService.IfNotExistsAddVertex(synonymOfWord);
        _graphService.IfNotExistsAddEdge(wordVertex, synonymVertex);
    }

    public async Task AddSynonymAsync(string word, string synonymOfWord)
    {
        SynonymVertex wordVertex = _graphService.IfNotExistsAddVertex(word);
        SynonymVertex synonymVertex = _graphService.IfNotExistsAddVertex(synonymOfWord);
        _graphService.IfNotExistsAddEdge(wordVertex, synonymVertex);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public void ClearAllSynonyms()
    {
        _graphService.ClearGraph();
    }

    /// <inheritdoc/>
    public List<SynonymVertex> GetAllSynonyms()
    {
        return _graphService.GetAllVertices();
    }

    /// <inheritdoc/>
    public List<string>? GetAllSynonymsByWord(string word)
    {
        SynonymVertex? synonymVertex = _graphService.Search(word);

        if (synonymVertex != null)
        {
            List<string> allSynonyms = _graphService
                .GetAllConnectedVertices(synonymVertex)
                .ConvertAll(synonymVertex => synonymVertex.Value);

            _ = allSynonyms.Remove(word);
            return allSynonyms;
        }

        return default;
    }

    /// <inheritdoc/>
    public void LoadSynonymsIntoState(List<SynonymVertex> synonyms)
    {
        _graphService.LoadListIntoGraph(synonyms);
    }
}
