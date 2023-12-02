using RestTest.Application.Synonyms.Dtos;

namespace RestTest.Web.Models.Views.Synonym;

public class TargetSynonymView
{
    public string FromSynonym { get; }

    public List<string>? Synonyms { get; }

    public TargetSynonymView(TargetSynonymDto dto)
    {
        FromSynonym = dto.FromSynonym;
        Synonyms = dto.Synonyms;
    }
}
