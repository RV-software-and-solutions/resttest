using FluentValidation;

namespace RestTest.Application.ResourceItems.Commands.UploadImage;
public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(v => v.Image)
            .NotEmpty();
    }
}
