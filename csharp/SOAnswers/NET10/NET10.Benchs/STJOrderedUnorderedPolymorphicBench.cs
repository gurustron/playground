using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs;

[MemoryDiagnoser]
public class STJOrderedUnorderedPolymorphicBench
{
    private const string OrderedData 
        = """{"$type":"MyDescendant1","Prop2":2,"Prop3":"3qwerqwrewqrqwrqwerqwerqwrqwer ewqrewrqwerwrweqrqwe  rwe rweqr qwer r","Prop4":4,"Prop1":1}""";
    private const string UnOrdered 
        = """{"Prop2":2,"Prop3":"3qwerqwrewqrqwrqwerqwerqwrqwer ewqrewrqwerwrweqrqwe  rwe rweqr qwer r","Prop4":4,"Prop1":1,"$type":"MyDescendant1"}""";

    private static readonly JsonSerializerOptions Default = new();
    private static readonly JsonSerializerOptions AllowUnordered = new() { AllowOutOfOrderMetadataProperties = true };


    [Benchmark]
    public MyBase? OrderedAllowUnordered()
        => JsonSerializer.Deserialize<MyBase>(OrderedData, AllowUnordered);

    [Benchmark]
    public MyBase? UnOrderedAllowUnordered()
        => JsonSerializer.Deserialize<MyBase>(UnOrdered, AllowUnordered);

    [Benchmark]
    public MyBase? Ordered() 
        => JsonSerializer.Deserialize<MyBase>(OrderedData, Default);

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(MyDescendant1), nameof(MyDescendant1))]
    public class MyBase
    {
        public int Prop1 { get; set; }
    }

    public class MyDescendant1 : MyBase
    {
        public int Prop2 { get; set; }
        public string Prop3 { get; set; }
        public long Prop4 { get; set; }
    }
}