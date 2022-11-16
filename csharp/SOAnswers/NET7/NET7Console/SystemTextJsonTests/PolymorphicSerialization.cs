using System.Text.Json;
using System.Text.Json.Serialization;

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

        var json = JsonSerializer.Serialize(container, new JsonSerializerOptions { WriteIndented = true });
        var @base = JsonSerializer.Deserialize<Root<Base>>(json);
    }
    
    [JsonDerivedType(typeof(Child1), typeDiscriminator: nameof(Child1))]
    [JsonDerivedType(typeof(ChildChild1), typeDiscriminator: nameof(ChildChild1))]
    public class Base
    {
        public int BaseI { get; set; }
    }
    
    [JsonDerivedType(typeof(ChildChild1), typeDiscriminator: nameof(ChildChild1))] // does not work for base
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
}