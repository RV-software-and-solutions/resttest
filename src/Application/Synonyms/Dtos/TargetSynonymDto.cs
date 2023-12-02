namespace RestTest.Application.Synonyms.Dtos;
public class TargetSynonymDto
{
    public required string FromSynonym { get; set; }

    public required List<string>? Synonyms { get; set; }
}
