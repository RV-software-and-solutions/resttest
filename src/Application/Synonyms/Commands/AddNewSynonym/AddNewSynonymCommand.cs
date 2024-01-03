using RestTest.Application.Common.Models;
using RestTest.Application.Services;

namespace RestTest.Application.Synonyms.Commands.AddNewSynonym;
public record AddNewSynonymCommand(string SynonymFrom, string SynonymTo) : IRequest<Result>;

public class AddNewSynonymCommandHandler : IRequestHandler<AddNewSynonymCommand, Result>
{
    private readonly ISynonymService _synonymService;
    public AddNewSynonymCommandHandler(ISynonymService synonymService)
    {
        _synonymService = synonymService;
    }

    public async Task<Result> Handle(AddNewSynonymCommand request, CancellationToken cancellationToken)
    {
        await _synonymService.AddSynonymAsync(request.SynonymFrom, request.SynonymTo);
        return Result.Success();
    }
}
