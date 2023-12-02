using RestTest.Core.Services.Graph.Models;

namespace RestTest.Domain.Dtos.Synonym;
public class SynonymVertex : AbstractVertex<string>
{
    public SynonymVertex()
    {
    }

    public SynonymVertex(string value)
    {
        InitVertex(value);
    }
}
