using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Common.Models;
using RestTest.Application.ResourceItems.Commands.CreateResourceItem;
using RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;

namespace RestTest.Web.Controllers;

[Authorize]
public class ResourceItemsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<ResourceItemDto>>> GetResourceItemsWithPagination([FromQuery] GetResourceItemsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateResourceItemCommand command)
    {
        return await Mediator.Send(command);
    }
}
