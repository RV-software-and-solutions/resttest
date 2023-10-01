using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.ResourceItems.Commands.CreateResourceItem;

namespace Web.Controllers;

[Authorize]
public class ResourceItemsController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateResourceItemCommand command)
    {
        return await Mediator.Send(command);
    }
}
