using RestTest.Application.Common.Models;
using RestTest.Application.Services;

namespace RestTest.Application.Synonyms.Commands;
public record AddNewSynonymCommand(string SynonymFrom, string SynonymTo) : IRequest<Result>;

public class AddNewSynonymCommandHandler : IRequestHandler<AddNewSynonymCommand, Result>
{
    private readonly ISynonymService _synonymService;
    public AddNewSynonymCommandHandler(ISynonymService synonymService)
    {
        _synonymService = synonymService;
    }

    public Task<Result> Handle(AddNewSynonymCommand request, CancellationToken cancellationToken)
    {
        _synonymService.AddSynonym(request.SynonymFrom, request.SynonymTo);
        return Task.FromResult(Result.Success());
    }
}
