using RestTest.Core.Services.ResourceManager.Core;

namespace RestTest.Core.Services.ResourceManager.FileManager;
public class FileResource : IResource, IDisposable
{
    protected readonly Stream _file;
    public string Path { get; }

    public FileResource(Stream stream, string path)
    {
        _file = stream;
        Path = path;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _file.Dispose();
        }
    }

    public void Download(string toPath)
    {
        using var stream = GetStream();
        using var output = new FileStream(toPath, FileMode.Create);

        stream.CopyTo(output);
    }

    public string GetContents()
    {
        using StreamReader reader = new(_file);
        return reader.ReadToEnd();
    }

    public Stream GetStream()
    {
        return _file;
    }
}
