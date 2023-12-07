using RestTest.Web.Models.Requests.Synonym;

namespace RestTest.Web.FluentValidators.Synonym;

public class AddNewSynonymRequestValidator : ExtendAbstractValidator<AddNewSynonymRequest>
{
    public AddNewSynonymRequestValidator()
    {
        NotEmptyValue(x => x.SynonymFrom);
        NotEmptyValue(x => x.SynonymTo);
    }
}
