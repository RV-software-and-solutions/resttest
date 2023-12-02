using FluentValidation;

namespace RestTest.Application.Synonyms.Commands;

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
