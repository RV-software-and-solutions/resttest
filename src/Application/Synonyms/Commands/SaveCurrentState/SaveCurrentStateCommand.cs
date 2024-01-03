using RestTest.Application.Common.Models;
using RestTest.Application.Services;
using RestTest.Core.Services.AwsDynamo;
using RestTest.Domain.Entities;

namespace RestTest.Application.Synonyms.Commands.SaveCurrentState;

public record SaveCurrentStateCommand() : IRequest<Result>;

public class SaveCurrentStateCommandHandler : IRequestHandler<SaveCurrentStateCommand, Result>
{
    private const string SYNONYM_DYNAMO_TABLE_NAME = "synonyms";

    private readonly ISynonymService _synonymService;
    private readonly IAwsDynamoService _awsDynamoService;
    public SaveCurrentStateCommandHandler(ISynonymService synonymService, IAwsDynamoService awsDynamoService)
    {
        _synonymService = synonymService;
        _awsDynamoService = awsDynamoService;
    }

    public async Task<Result> Handle(SaveCurrentStateCommand request, CancellationToken cancellationToken)
    {
        await _awsDynamoService.DeleteItemsAsBatch<VertexItem>(SYNONYM_DYNAMO_TABLE_NAME);

        List<VertexItem> allSynonyms = _synonymService.GetAllSynonyms().ConvertAll(synonym => new VertexItem()
        {
            Id = synonym.Id,
            Value = synonym.Value,
            AdjacentIds = synonym.Adjacent?.ConvertAll(v => v.Id),
        });

        await _awsDynamoService.SaveItemsAsBatch(SYNONYM_DYNAMO_TABLE_NAME, allSynonyms);
        return Result.Success();
    }
}
