using BenchmarkDotNet.Attributes;

namespace NET9.Benchs;

[MemoryDiagnoser]
public class DynamicBenchmark
{
    private List<int> IntList { get; set; } = new List<int>();
    private List<Guid> GuidList { get; set; } = new List<Guid>();

    int i = 32;
    dynamic iD = 32;
    object iO = 32;
    [Benchmark]
    public string GetIntValue() => i.ToString();

    [Benchmark]
    public string GetIntValueDynamic() => iD.ToString();

    [Benchmark]
    public string GetIntValueObject() => iO.ToString();

    // [GlobalCleanup]
    // public void GlobalSetup()
    // {
    //     IntList = Enumerable.Range(0, 100).ToList();
    //     for (var i = 0; i < IntList.Count; i++)
    //     {
    //         GuidList .Add(Guid.Empty);
    //     }
    // }

    // [Benchmark]
    // public dynamic GetIntValue()
    // {
    //     return IntList.Find(a => a == 50);
    // }

    // [Benchmark]
    // public int GetValueExplicit()
    // {
    //     return IntList.Find(a => a == 50);
    // }

    // [Benchmark]
    // public dynamic GetGuidValue()
    // {
    //     return GuidList.Find(a => a == Guid.Empty);
    // }

    // [Benchmark]
    // public Guid GetGuidValueExplicit()
    // {
    //     return GuidList.Find(a => a == Guid.Empty);
    // }
}