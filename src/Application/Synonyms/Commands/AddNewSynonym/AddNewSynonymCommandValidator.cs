using FluentValidation;

namespace RestTest.Application.Synonyms.Commands.AddNewSynonym;

public class AddNewSynonymCommandValidator : AbstractValidator<AddNewSynonymCommand>
{
    public AddNewSynonymCommandValidator()
    {
        RuleFor(v => v.SynonymFrom)
            .NotEmpty();

        RuleFor(v => v.SynonymTo)
            .NotEmpty();
    }
}
