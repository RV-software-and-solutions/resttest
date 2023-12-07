using System.Text;
using Amazon.S3.Model;
using RestTest.Core.Services.ResourceManager.Core;

namespace RestTest.Core.Services.ResourceManager.S3;

public class S3Resource : IResource, IDisposable
{
    protected readonly GetObjectResponse _response;

    public string Path { get; }

    public S3Resource(GetObjectResponse response, string path)
    {
        _response = response;
        Path = path;
    }

    public string GetContents()
    {
        using var stream = GetStream();
        using var memStream = new MemoryStream();

        stream.CopyTo(memStream);
        memStream.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(memStream, Encoding.ASCII);

        return reader.ReadToEnd();
    }

    public Stream GetStream()
    {
        return _response.ResponseStream;
    }

    public void Download(string toPath)
    {
        using var stream = GetStream();
        using var output = new FileStream(toPath, FileMode.Create);

        stream.CopyTo(output);
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
            _response.Dispose();
        }
    }
}
