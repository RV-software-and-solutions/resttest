using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Common.Models;
using RestTest.Application.Synonyms.Commands.AddNewSynonym;
using RestTest.Application.Synonyms.Commands.LoadState;
using RestTest.Application.Synonyms.Commands.ResetState;
using RestTest.Application.Synonyms.Commands.SaveCurrentState;
using RestTest.Application.Synonyms.Dtos;
using RestTest.Application.Synonyms.Queries.GetSynonymsByWord;
using RestTest.Web.Models.Requests.Synonym;
using RestTest.Web.Models.Views.Synonym;

namespace RestTest.Web.Controllers;

public class SynonymController : ApiControllerBase
{
    /// <summary>
    /// Retrieves synonyms for a specified word.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This endpoint is used to get a list of synonyms for a given word. 
    /// The word is provided as a query parameter.
    /// </para>
    /// Sample request:
    /// 
    ///     GET /synonym?fromSynonym=A
    /// </remarks>
    /// <param name="request">The word for which synonyms are being requested.</param>
    /// <response code="200">Returns a list of synonyms for the specified word.</response>
    [HttpGet]
    [ProducesResponseType(typeof(TargetSynonymView), StatusCodes.Status200OK)]
    public async Task<ActionResult<TargetSynonymView>> GetTargetSynonyms([FromQuery] GetTargetSynonymRequest request)
    {
        TargetSynonymDto dto = await Mediator.Send(new GetAllSynonymsByWordQuery(request.FromSynonym));
        return Ok(new TargetSynonymView(dto));
    }

    /// <summary>
    /// Adds a new synonym pair to the system.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This endpoint is used to add a new synonym relationship between two words. 
    /// It requires specifying both words in the synonym relationship.
    /// </para>
    /// Sample request:
    /// 
    ///     POST /synonym
    ///     {
    ///         "synonymFrom": "A",
    ///         "synonymTo": "B"
    ///     }
    ///     
    /// </remarks>
    /// <param name="request">The synonym pair to add.</param>
    /// <response code="200">Indicates the synonym pair was successfully added.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<ActionResult> AddSynonym(AddNewSynonymRequest request)
    {
        await Mediator.Send(new AddNewSynonymCommand(request.SynonymFrom, request.SynonymTo));
        return Ok();
    }

    /// <summary>
    /// Saves the current state of the application.
    /// </summary>
    /// <remarks>
    /// This endpoint triggers the saving of the current application state. 
    /// It could involve persisting current configurations, user data, or any other relevant stateful information.
    /// 
    /// Sample request:
    /// 
    ///     POST /synonym/save-current-state
    ///     
    /// </remarks>
    /// <response code="200">Returns a result object indicating the success or failure of the save operation.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result>> SaveCurrentState()
    {
        Result result = await Mediator.Send(new SaveCurrentStateCommand());
        return Ok(result);
    }

    /// <summary>
    /// Loads the previously saved state of the application.
    /// </summary>
    /// <remarks>
    /// This endpoint is used to load a previously saved state of the application. 
    /// It might involve restoring configurations, user data, or other relevant stateful information 
    /// that has been persisted earlier.
    /// Sample request:
    /// 
    ///     POST /synonym/load-state
    /// </remarks>
    /// <response code="200">Returns a result object indicating the success or failure of the load operation.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result>> LoadState()
    {
        Result result = await Mediator.Send(new LoadStateCommand());
        return Ok(result);
    }

    /// <summary>
    /// Resets the application state.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this endpoint to reset the state of the application. This operation might involve clearing caches, 
    /// resetting session data, or any other stateful components of the application.
    /// </para>
    /// <para><b>Warning:</b> This operation cannot be undone and will result in the loss of the current state.</para>
    /// 
    /// Sample request:
    /// 
    ///     POST synoynm/reset-state
    /// 
    /// </remarks>
    /// <response code="200">Returns the result of the reset operation, indicating success.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result>> ResetState()
    {
        Result result = await Mediator.Send(new ResetStateCommand());
        return Ok(result);
    }
}
