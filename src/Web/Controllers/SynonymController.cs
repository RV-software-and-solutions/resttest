using Microsoft.AspNetCore.Mvc;
using RestTest.Application.Synonyms.Commands;
using RestTest.Application.Synonyms.Dtos;
using RestTest.Application.Synonyms.Queries.GetSynonymsByWord;
using RestTest.Web.Models.Requests.Synonym;
using RestTest.Web.Models.Views.Synonym;

namespace RestTest.Web.Controllers;

public class SynonymController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TargetSynonymView>> GetTargetSynonyms([FromQuery] string fromWord)
    {
        TargetSynonymDto dto = await Mediator.Send(new GetAllSynonymsByWordQuery(fromWord));
        return Ok(new TargetSynonymView(dto));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AddSynonym(AddNewSynonymRequest request)
    {
        await Mediator.Send(new AddNewSynonymCommand(request.SynonymFrom, request.SynonymTo));
        return Ok();
    }
}
