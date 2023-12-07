using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentAssertions;
using RestTest.Web.Models.Requests.Auth;
using Web.IntegrationTests.Core;
using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Auth;

[ExcludeFromCodeCoverage]
internal class AuthControllerTests : BaseTesting
{
    [Test]
    public async Task LoginUser_Should_Return_Unauthorized()
    {
        HttpResponseMessage result = await Client.PostAsync("api/auth/login",
            ContentHelper.GetStringContent(new UserLoginRequest()
            {
                Password = "Marko1992c#",
                UserName = "WrongEmail@test.com",
            }));

        result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
