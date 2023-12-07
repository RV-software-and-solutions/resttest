using FluentValidation;

namespace RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;
public class GetResourceItemsWithPaginationQueryValidator : AbstractValidator<GetResourceItemsWithPaginationQuery>
{
    public GetResourceItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
                    .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
