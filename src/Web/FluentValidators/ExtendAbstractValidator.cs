using System.Linq.Expressions;
using FluentValidation;

namespace RestTest.Web.FluentValidators;

public abstract class ExtendAbstractValidator<T> : AbstractValidator<T>
{
    public ExtendAbstractValidator<T> NotEmptyValue(Expression<Func<T, string>> expression)
    {
        RuleFor(expression)
            .NotEmpty()
            .WithMessage($"{typeof(T).FullName} is required!");

        return this;
    }
}
