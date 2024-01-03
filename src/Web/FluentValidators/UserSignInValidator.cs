using RestTest.Web.Models.Requests.Auth;

namespace RestTest.Web.FluentValidators;

public class UserSignInValidator : ExtendAbstractValidator<SignUpCommandRequst>
{
    public UserSignInValidator()
    {
        NotEmptyValue(x => x.UserName);
        NotEmptyValue(x => x.EmailAddress);
        NotEmptyValue(x => x.Password);
    }
}
