using System.Linq.Expressions;
using FluentValidation;

namespace RestTest.Web.FluentValidators;

public abstract class ExtendAbstractValidator<T> : AbstractValidator<T>
{
    public ExtendAbstractValidator<T> NotEmptyValue(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage($" is required!");

        return this;
    }
}
