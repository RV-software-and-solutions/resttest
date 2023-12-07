using RestTest.Web.Models.Requests.Synonym;

namespace RestTest.Web.FluentValidators.Synonym;

public class GetTargetSynonymRequestValidator : ExtendAbstractValidator<GetTargetSynonymRequest>
{
    public GetTargetSynonymRequestValidator()
    {
        NotEmptyValue(x => x.FromSynonym);
    }
}
