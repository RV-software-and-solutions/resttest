using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using RestTest.Core.Services.ResourceManager.Configuration;
using RestTest.Core.Services.ResourceManager.Core;

namespace RestTest.Core.Services.ResourceManager.S3;

public class S3ResourceManager : IResourceManager<S3Resource>
{
    public string BucketName { get; }

    public RegionEndpoint Region { get; }

    public int DefaultTimeoutInMinutes { get; set; }

    protected AmazonS3Client _client;

    public S3ResourceManager(IOptions<S3Configuration> configuration)
    {
        BucketName = configuration.Value.BucketName;
        Region = configuration.Value.RegionEndpoint;
        DefaultTimeoutInMinutes = configuration.Value.DefaultTimeoutInMinutes;
        int maxErrorRetry = configuration.Value.ClientMaxErrorRetry;

        _client = new AmazonS3Client(new AmazonS3Config()
        {
            RegionEndpoint = Region,
            RetryMode = Amazon.Runtime.RequestRetryMode.Standard,
            MaxErrorRetry = maxErrorRetry
        });
    }

    public async Task<bool> Exists(string path)
    {
        var result = await _client.GetObjectMetadataAsync(BucketName, path);
        return result.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<IResource?> Get(string path)
    {
        try
        {
            var data = await _client.GetObjectAsync(BucketName, path);

            if (data.HttpStatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            return new S3Resource(data, path);
        }
        catch (AmazonS3Exception ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            throw;
        }
    }

    public async Task<bool> Write(Stream input, string path)
    {
        var response = await _client.PutObjectAsync(new PutObjectRequest()
        {
            BucketName = BucketName,
            Key = path,
            InputStream = input
        });

        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> Move(string sourcePath, string destinationPath)
    {
        var request = new CopyObjectRequest
        {
            SourceBucket = BucketName,
            SourceKey = sourcePath,
            DestinationBucket = BucketName,
            DestinationKey = destinationPath
        };
        CopyObjectResponse response = await _client.CopyObjectAsync(request);

        bool isSuccess = response.HttpStatusCode == HttpStatusCode.OK;

        if (isSuccess)
        {
            return await Delete(sourcePath);
        }

        return isSuccess;
    }

    public async Task<bool> Delete(string path)
    {
        var response = await _client.DeleteObjectAsync(BucketName, path);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public Task<string?> GetPublicUrl(string path)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = BucketName,
            Key = path,
            Expires = DateTime.UtcNow.AddMinutes(DefaultTimeoutInMinutes)
        };

        return Task.FromResult(_client.GetPreSignedURL(request))!;
    }
}
