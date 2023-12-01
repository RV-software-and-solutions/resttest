using RestTest.Core.Services.Graph.Models;

namespace RestTest.Domain.Dtos.Synonym;
public class SynonymVertex : AbstractVertex<string>
{
    public SynonymVertex(string value) : base(value)
    {
    }
}
