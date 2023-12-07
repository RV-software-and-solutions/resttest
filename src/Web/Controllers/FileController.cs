using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestTest.Application.ResourceItems.Commands.UploadImage;
using RestTest.Web.Models.Requests.File;

namespace RestTest.Web.Controllers;

/// <summary>
/// Controller for file operations.
/// </summary>
/// <remarks>
/// This controller handles file-related actions such as uploading files.
/// </remarks>
[Authorize]
public class FileController : ApiControllerBase
{
    /// <summary>
    /// Uploads a file to the server.
    /// </summary>
    /// <remarks>
    /// Use this endpoint to upload a file. The file should be included in the request's form data.
    /// </remarks>
    /// <param name="uploadFileModel">The model representing the file to be uploaded.</param>
    /// <response code="200">Indicates that the file has been successfully uploaded.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Upload([FromForm] UploadFileRequestModel uploadFileModel)
    {
        await Mediator.Send(ToCommand(uploadFileModel));
        return Ok();
    }

    private static UploadImageCommand ToCommand([FromBody] UploadFileRequestModel uploadFileModel)
        => new(uploadFileModel.Image.OpenReadStream());
}
