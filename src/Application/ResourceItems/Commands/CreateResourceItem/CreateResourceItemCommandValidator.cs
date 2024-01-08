using FluentValidation;

namespace RestTest.Application.ResourceItems.Commands.CreateResourceItem;
public class CreateResourceItemCommandValidator : AbstractValidator<CreateResourceItemCommand>
{
    public CreateResourceItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200);

        RuleFor(v => v.Location)
            .MaximumLength(200);
    }
}
