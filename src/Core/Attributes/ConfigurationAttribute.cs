namespace RestTest.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConfigurationAttribute : Attribute
{
    public required string Key { get; set; }
}

public static class ConfigurationAttributeExtension
{
    public static T GetAttribute<T>(this Type type) where T : Attribute
    {
        T? attribute = Attribute.GetCustomAttribute(type, typeof(T)) as T;

        return attribute ?? throw new ArgumentException($"The class {type} does not contain attribute {typeof(T)}.");
    }
}
