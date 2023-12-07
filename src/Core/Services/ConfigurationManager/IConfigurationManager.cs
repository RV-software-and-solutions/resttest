namespace RestTest.Core.Services.ConfigurationManager;
public interface IConfigurationManager
{
    Task InitConfigurationManager();
    string RetriveConfigValue(string key);
    string? this[string key] { get; }
}
