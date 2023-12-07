using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Common.Models;
using RestTest.Application.ResourceItems.Commands.CreateResourceItem;
using RestTest.Application.ResourceItems.Dtos;
using RestTest.Application.ResourceItems.Queries.GetResourceItemsWithPagination;

namespace RestTest.Web.Controllers;

/// <summary>
/// Controller for managing resource items.
/// </summary>
/// <remarks>
/// This controller contains actions for retrieving and creating resource items.
/// </remarks>
[Authorize]
public class ResourceItemsController : ApiControllerBase
{
    /// <summary>
    /// Retrieves a paginated list of resource items.
    /// </summary>
    /// <remarks>
    /// Use this endpoint to fetch a paginated list of resource items. 
    /// You can specify pagination parameters in the query.
    /// </remarks>
    /// <param name="query">The pagination query parameters.</param>
    /// <response code="200">Returns a paginated list of resource items.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<ResourceItemDto>>> GetResourceItemsWithPagination([FromQuery] GetResourceItemsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Creates a new resource item.
    /// </summary>
    /// <remarks>
    /// This endpoint is used for creating a new resource item. 
    /// Provide the necessary details in the request body.
    /// </remarks>
    /// <param name="command">The command to create a new resource item.</param>
    /// <response code="200">Returns the ID of the newly created resource item.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> Create(CreateResourceItemCommand command)
    {
        return await Mediator.Send(command);
    }
}
