using Microsoft.AspNetCore.Mvc;
using RestTest.Application.ResourceItems.Commands.UploadImage;
using RestTest.Web.Models.Requests;

namespace RestTest.Web.Controllers;

public class FileController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Upload([FromForm] UploadFileRequestModel uploadFileModel)
    {
        await Mediator.Send(ToCommand(uploadFileModel));
        return Ok();
    }

    private static UploadImageCommand ToCommand([FromBody] UploadFileRequestModel uploadFileModel)
        => new()
        {
            Image = uploadFileModel.Image.OpenReadStream(),
        };
}
