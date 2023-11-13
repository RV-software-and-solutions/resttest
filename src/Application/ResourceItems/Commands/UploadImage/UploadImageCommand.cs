using MediatR;
using RestTest.Application.Common.Models;
using RestTest.Core.Services.ConfigurationManager;
using RestTest.Core.Services.ConfigurationManager.ParameterStore;
using RestTest.Core.Services.ResourceManager.Core;
using RestTest.Core.Services.ResourceManager.FileManager;
using RestTest.Core.Services.ResourceManager.S3;

namespace RestTest.Application.ResourceItems.Commands.UploadImage;
public class UploadImageCommand : IRequest
{
    public required Stream Image { get; set; }
}

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand>
{
    private readonly IResourceManager<S3Resource> _s3ResourceManager;
    private readonly IResourceManager<FileResource> _fileResourceManager;

    private readonly IConfigurationManager _configurationManager;

    public UploadImageCommandHandler(IResourceManager<S3Resource> s3ResourceManager, IResourceManager<FileResource> fileResourceManager,
        IAwsParameterStoreManager awsParameterStoreManager)
    {
        _s3ResourceManager = s3ResourceManager;
        _fileResourceManager = fileResourceManager;

        _configurationManager = awsParameterStoreManager;
    }

    public async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        using Stream imageStream = new MemoryStream();
        await request.Image.CopyToAsync(imageStream, cancellationToken);
        //bool uploadOperation = await _s3ResourceManager.Write(request.Image, GetNewImagePath());
        //if (!uploadOperation)
        //{
        //    Result.Failure(new[] { "" });
        //}

        bool saveOperation = await _fileResourceManager.Write(imageStream, GetNewImageLocalPath());
        if (!saveOperation)
        {
            Result.Failure(new[] { "" });
        }
    }

    private static string GetNewImageName() => Guid.NewGuid().ToString();

    private static string GetNewImagePath() => $"images/{GetNewImageName()}.png";

    private string GetNewImageLocalPath() => $"{_configurationManager["LocalFileLocation"]!}/{GetNewImagePath()}";

}
