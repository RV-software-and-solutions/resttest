using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Auth.Commands.LoginUser;
using RestTest.Application.Auth.Commands.SignupUser;
using RestTest.Domain.Dtos.Auth;
using RestTest.Web.Models.Requests;
using RestTest.Web.Models.Requests.Auth;
using RestTest.Web.Models.Views.Auth;

namespace RestTest.Web.Controllers;

public class AuthController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> LoginUser([FromBody] UserLoginRequest userLoginRequest)
    {
        AuthResponseDto dto = await Mediator.Send(ToCommand(userLoginRequest));
        return Ok(new AuthResponseView(dto));
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SignInUser([FromBody] UserSignUpRequst userSignUpRequest)
    {
        SignUpUserResponseDto dto = await Mediator.Send(ToCommand(userSignUpRequest));
        return Ok(dto);
    }

    private static LoginUserCommand ToCommand(UserLoginRequest request)
        => new(request.UserName, request.Password);

    private static SignUpUserCommand ToCommand(UserSignUpRequst request)
        => new(request.EmailAddress, request.UserName, request.Password);
}
