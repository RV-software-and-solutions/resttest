using RestTest.Application.Common.Models;
using RestTest.Application.Services;
using RestTest.Core.Services.AwsDynamo;
using RestTest.Domain.Entities;

namespace RestTest.Application.Synonyms.Commands.ResetState;
public record ResetStateCommand() : IRequest<Result>;

public class ResetStateCommandHandler : IRequestHandler<ResetStateCommand, Result>
{
    private const string SYNONYM_DYNAMO_TABLE_NAME = "synonyms";

    private readonly ISynonymService _synonymService;
    private readonly IAwsDynamoService _awsDynamoService;
    public ResetStateCommandHandler(ISynonymService synonymService, IAwsDynamoService awsDynamoService)
    {
        _synonymService = synonymService;
        _awsDynamoService = awsDynamoService;
    }

    public async Task<Result> Handle(ResetStateCommand request, CancellationToken cancellationToken)
    {
        await _awsDynamoService.DeleteItemsAsBatch<VertexItem>(SYNONYM_DYNAMO_TABLE_NAME);
        _synonymService.ClearAllSynonyms();
        return await Task.FromResult(Result.Success());
    }
}
