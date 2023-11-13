using RestTest.Core.Services.ResourceManager.Core;

namespace RestTest.Core.Services.ResourceManager.FileManager;
public class FileResourceManager : IResourceManager<FileResource>
{
    public async Task<bool> Delete(string path)
    {
        bool fileExists = await Exists(path);
        if (fileExists)
        {
            await Task.Run(() => File.Delete(path));
            return true;
        }

        return false;
    }

    public Task<bool> Exists(string path)
    {
        return Task.FromResult(File.Exists(path));
    }

    public async Task<IResource?> Get(string path)
    {
        try
        {
            return await Task.Run(() => new FileResource(File.OpenRead(path), path));
        }
        catch (Exception ex)
        {
            await Task.FromException(ex);
        }

        return null;
    }

    public async Task<string?> GetPublicUrl(string path)
    {
        return await Task.Run(() => path);
    }

    public async Task<bool> Move(string sourcePath, string destinationPath)
    {
        await Task.Run(() => File.Move(sourcePath, destinationPath));
        return true;
    }

    public async Task<bool> Write(Stream input, string path)
    {
        input.Seek(0, SeekOrigin.Begin);

        CreateDirectoryIfNotExists(path);

        using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await input.CopyToAsync(fileStream);
        return true;
    }

    private void CreateDirectoryIfNotExists(string fullFilePath)
    {
        string fileLocationWithoutFileName = fullFilePath.Remove(fullFilePath.LastIndexOf('/'), fullFilePath.Length - fullFilePath.LastIndexOf('/'));

        Directory.CreateDirectory(fileLocationWithoutFileName);
    }
}
