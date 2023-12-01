using RestTest.Core.Services.Graph;
using RestTest.Domain.Dtos.Synonym;

namespace RestTest.Application.Services;
public class SynonymService : ISynonymService
{
    public readonly IGraphService<SynonymVertex, string> _graphService;

    public SynonymService(IGraphService<SynonymVertex, string> graphService)
    {
        _graphService = graphService;
    }

    public void AddSynonym(string word, string synonymOfWord)
    {
        SynonymVertex wordVertex = _graphService.IfNotExistsAddVertex(word);
        SynonymVertex synonymVertex = _graphService.IfNotExistsAddVertex(synonymOfWord);
        _graphService.IfNotExistsAddEdge(wordVertex, synonymVertex);
    }

    public List<string>? GetAllSynonymsOfWord(string word)
    {
        SynonymVertex? synonymVertex = _graphService.Search(word);

        if (synonymVertex != null)
        {
            return _graphService
                .GetAllConnectedVertices(synonymVertex)
                .ConvertAll(synonymVertex => synonymVertex.Value);
        }

        return default;
    }
}
