using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace NET7Console.SystemTextJsonTests;

public class RequiredMembers
{
    public static void Do()
    {
        try
        {
            var myClass = JsonSerializer.Deserialize<MyClass>(
                """
                {
                    
                }
                """);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            var myClass1 = JsonSerializer.Deserialize<MyClass1>(
                """
                {
                    
                }
                """);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

var settings = new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { ThrowNullableRequired }
    }
};
    
try
{
    var myClass1 = JsonSerializer.Deserialize<Car>(
        """
        {
            "Name":null,
            "Prefix":null
        }
        """, settings);
}
catch (Exception e)
{
    Console.WriteLine(e);
}
var car = JsonSerializer.Deserialize<Car>(
    """
        {
            "Name":"a",
            "Prefix":null
        }
        """, settings);
    }

    private static void ThrowNullableRequired(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        foreach (var property in jsonTypeInfo.Properties)
        {
            if (property.IsRequired && !property.PropertyType.IsValueType && property.Set is { } setter)
            {
                NullabilityInfoContext context = new();
                var nullabilityInfo = context.Create(jsonTypeInfo.Type.GetProperty(property.Name));
                if (nullabilityInfo.WriteState is not NullabilityState.NotNull)
                {
                    continue;
                }
                property.Set = (obj, val) =>
                {
                    if (val is null)
                    {
                        throw new JsonException($"null for required prop: {property.Name}");
                    }

                    setter(obj, val);
                };
            }
        }
    }
}   

file class MyClass
{
    public required int Type { get; set; }
}
file class MyClass1
{
    [JsonRequired]
    public int Type { get; set; }
}

file class Car
{
    public required string Name { get; set; }
    public required string? Prefix { get; set; }
}