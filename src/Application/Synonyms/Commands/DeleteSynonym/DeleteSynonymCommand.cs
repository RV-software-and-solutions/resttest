using RestTest.Application.Common.Models;

namespace RestTest.Application.Synonyms.Commands.DeleteSynonym;

public record DeleteSynonymCommand() : IRequest<Result>;
