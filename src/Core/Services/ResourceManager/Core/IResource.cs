namespace RestTest.Core.Services.ResourceManager.Core;

public interface IResource
{
    string GetContents();

    Stream GetStream();

    void Download(string toPath);
}
