using RestTest.Application.Services;
using RestTest.Application.Synonyms.Dtos;

namespace RestTest.Application.Synonyms.Queries.GetSynonymsByWord;

public record GetAllSynonymsByWordQuery(string Synonym) : IRequest<TargetSynonymDto>;

public class GetAllSynonymsByWordQueryHandler : IRequestHandler<GetAllSynonymsByWordQuery, TargetSynonymDto>
{
    private readonly ISynonymService _synonymService;
    public GetAllSynonymsByWordQueryHandler(ISynonymService synonymService)
    {
        _synonymService = synonymService;
    }

    public Task<TargetSynonymDto> Handle(GetAllSynonymsByWordQuery request, CancellationToken cancellationToken)
    {
        List<string>? allTargetSynonyms = _synonymService.GetAllSynonymsOfWord(request.Synonym);
        TargetSynonymDto targetSynonymDto = new()
        {
            FromSynonym = request.Synonym,
            Synonyms = allTargetSynonyms,
        };

        return Task.FromResult(targetSynonymDto);
    }
}
