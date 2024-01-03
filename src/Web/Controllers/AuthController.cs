using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Auth.Commands.LoginUser;
using RestTest.Application.Auth.Commands.SignUpUser;
using RestTest.Core.Dtos.Auth;
using RestTest.Web.Models.Requests.Auth;
using RestTest.Web.Models.Views.Auth;

namespace RestTest.Web.Controllers;

[Authorize]
public class AuthController : ApiControllerBase
{
    /// <summary>
    /// Authenticates a user and provides a token.
    /// </summary>
    /// <remarks>
    /// Use this endpoint for user login. The endpoint expects the user's credentials in the request body.
    /// On successful authentication, it returns a token for accessing secured resources.
    /// </remarks>
    /// <param name="userLoginRequest">The user's login credentials.</param>
    /// <response code="200">Returns an authentication token if the login is successful.</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        AuthResponseDto dto = await Mediator.Send(ToCommand(userLoginRequest));
        return Ok(new AuthResponseView(dto));
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <remarks>
    /// Use this endpoint for user registration. It accepts user details in the request body and creates a new user.
    /// </remarks>
    /// <param name="signUpUserRequest">The new user's sign-up details.</param>
    /// <response code="200">Indicates successful registration of the user.</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> SignUp([FromBody] SignUpCommandRequst signUpUserRequest)
    {
        SignUpUserResponseDto dto = await Mediator.Send(ToCommand(signUpUserRequest));
        return Ok(dto);
    }

    private static LoginUserCommand ToCommand(UserLoginRequest request)
        => new(request.UserName, request.Password);

    private static SignUpUserCommand ToCommand(SignUpCommandRequst request)
        => new(request.EmailAddress, request.UserName, request.Password);
}
