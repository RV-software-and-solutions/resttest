namespace RestTest.Web.Models.Requests.Auth;

public class SignUpCommandRequst
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? EmailAddress { get; set; }
}
