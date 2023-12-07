namespace RestTest.Web.Extensions;

public class CorsPolicyConfig
{
    public required string Name { get; set; }
    public required List<string> AllowedOrigins { get; set; }
}
