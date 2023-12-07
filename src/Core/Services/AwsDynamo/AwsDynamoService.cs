using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Options;
using RestTest.Core.Configuration;

namespace RestTest.Core.Services.AwsDynamo;
public class AwsDynamoService : IAwsDynamoService
{
    private readonly DynamoDBContext _context;
    private readonly AwsDynamoDbConfiguration _configuration;

    public AwsDynamoService(IOptions<AwsDynamoDbConfiguration> configuration)
    {
        _configuration = configuration.Value;
        AmazonDynamoDBClient client = new(_configuration.AccessKeyId, _configuration.SecretAccessKey, _configuration.RegionEndpoint);
        _context = new DynamoDBContext(client);
    }

    public async Task<List<T>> ReadAll<T>(string tableName)
    {
        var conditions = new List<ScanCondition>();
        return await _context.ScanAsync<T>(conditions, new DynamoDBOperationConfig
        {
            OverrideTableName = tableName,
        }).GetRemainingAsync();
    }

    public async Task Save<T>(string tableName, T item)
    {
        await _context.SaveAsync(item, new DynamoDBOperationConfig
        {
            OverrideTableName = tableName,
        });
    }

    public async Task SaveItemsAsBatch<T>(string tableName, List<T> items)
    {
        BatchWrite<T> batch = _context.CreateBatchWrite<T>(new DynamoDBOperationConfig
        {
            OverrideTableName = tableName,
        });
        batch.AddPutItems(items);

        await batch.ExecuteAsync();
    }

    public async Task DeleteItemsAsBatch<T>(string tableName)
    {
        List<T> items = await ReadAll<T>(tableName);
        BatchWrite<T> batch = _context.CreateBatchWrite<T>(new DynamoDBOperationConfig
        {
            OverrideTableName = tableName,
        });
        batch.AddDeleteItems(items);

        await batch.ExecuteAsync();
    }
}
