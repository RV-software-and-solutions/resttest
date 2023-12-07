namespace RestTest.Core.Services.ResourceManager.Core;
public interface IResourceManager<in T>
{
    Task<IResource?> Get(string path);

    Task<bool> Write(Stream input, string path);

    Task<bool> Move(string sourcePath, string destinationPath);

    Task<bool> Exists(string path);

    Task<bool> Delete(string path);

    Task<string?> GetPublicUrl(string path);
}
