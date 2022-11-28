using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace NET7Console.SystemTextJsonTests;

public class PolymorphicSerialization
{
    public static void Do()
    {
        var container = new Root<Base>()
        {
            Data = new ChildChild1
            {
                BaseI = 42,
                ChildI = 777,
                ChildChildI = 1
            }
        };

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { AddNestedDerivedTypes }
    }
};
        var json = JsonSerializer.Serialize(container,options);
        var @base = JsonSerializer.Deserialize<Root<Base>>(json, options);
    }
    
    [JsonDerivedType(typeof(Child1))]
    // [JsonDerivedType(typeof(Child1), typeDiscriminator: nameof(Child1))]
    // [JsonDerivedType(typeof(ChildChild1), typeDiscriminator: nameof(ChildChild1))]
    public class Base
    {
        public int BaseI { get; set; }
    }
    
    [JsonDerivedType(typeof(ChildChild1))] // does not work for base
    // [JsonDerivedType(typeof(ChildChild1), typeDiscriminator: nameof(ChildChild1))] // does not work for base
    public class Child1 : Base
    {
        public int ChildI { get; set; }
    }
    
    public class ChildChild1 : Child1
    {
        public int ChildChildI { get; set; }
    }
    
    public class Root<T>
    {
        public T Data { get; set; }
    }

    private static void AddNestedDerivedTypes(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.PolymorphismOptions is null)
        {
            return;
        }

        var derivedTypes = jsonTypeInfo.PolymorphismOptions.DerivedTypes
            .Where(t => t.DerivedType.GetCustomAttribute<JsonDerivedTypeAttribute>() != null)
            .Select(t => t.DerivedType)
            .ToList();

        var hashset = new HashSet<Type>(derivedTypes);
        var queue = new Queue<Type>(derivedTypes);
        while (queue.TryDequeue(out var derived))
        {
            if (!hashset.Contains(derived))
            {
                // Todo: handle discriminators
                jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(derived, derived.FullName));   
            }


            var attribute = derived.GetCustomAttributes<JsonDerivedTypeAttribute>();
            foreach (var jsonDerivedTypeAttribute in attribute)
            {
                queue.Enqueue(jsonDerivedTypeAttribute.DerivedType);
            }
        }
    }
}