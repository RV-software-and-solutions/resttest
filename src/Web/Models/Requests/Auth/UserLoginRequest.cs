using RestTest.Web.Models.Requests;
namespace RestTest.Web.Models.Requests.Auth;

public class UserLoginRequest
{
    public string UserName { get; set; }

    public string Password { get; set; }
}
