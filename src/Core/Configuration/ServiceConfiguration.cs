using RestTest.Core.Attributes;

namespace RestTest.Core.Configuration;

[Configuration(Key = "service")]
public class ServiceConfiguration
{
    public required string Name { get; set; }
}
