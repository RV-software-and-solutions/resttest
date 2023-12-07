namespace RestTest.Core.Services.AwsDynamo;
public interface IAwsDynamoService
{
    Task Save<T>(string tableName, T item);
    Task SaveItemsAsBatch<T>(string tableName, List<T> items);
    Task DeleteItemsAsBatch<T>(string tableName);
    Task<List<T>> ReadAll<T>(string tableName);
}
