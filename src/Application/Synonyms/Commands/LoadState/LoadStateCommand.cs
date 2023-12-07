using RestTest.Application.Common.Models;
using RestTest.Application.Services;
using RestTest.Core.Services.AwsDynamo;
using RestTest.Core.Services.Graph.Models;
using RestTest.Domain.Dtos.Synonym;
using RestTest.Domain.Entities;

namespace RestTest.Application.Synonyms.Commands.LoadState;

public record LoadStateCommand() : IRequest<Result>;

public class LoadStateCommandHandler : IRequestHandler<LoadStateCommand, Result>
{
    private readonly ISynonymService _synonymService;
    private readonly IAwsDynamoService _awsDynamoService;
    public LoadStateCommandHandler(ISynonymService synonymService, IAwsDynamoService awsDynamoService)
    {
        _synonymService = synonymService;
        _awsDynamoService = awsDynamoService;
    }

    public async Task<Result> Handle(LoadStateCommand request, CancellationToken cancellationToken)
    {
        List<VertexItem> synonymVertexItems = await _awsDynamoService.ReadAll<VertexItem>("synonyms");
        List<SynonymVertex> synonyms = synonymVertexItems.ConvertAll(synonymItem => new SynonymVertex()
        {
            Id = synonymItem.Id,
            Value = synonymItem.Value,
            Adjacent = LoadAdjcent(synonymItem.AdjacentIds),
        });

        _synonymService.LoadSynonymsIntoState(synonyms);
        return await Task.FromResult(Result.Success());
    }

    private static List<AbstractVertex<string>>? LoadAdjcent(List<string>? adjacentIds) =>
        adjacentIds?.ConvertAll(s => (AbstractVertex<string>)new SynonymVertex() { Id = s });
}
