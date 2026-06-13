using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

// namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
// [DisassemblyDiagnoser]
public class GrpcRepeatedFieldCollectionsBenchs
{
    [Params(1, 8, 9, 25, 299)]
    public int CollectionSize;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Strings = Enumerable.Range(1, CollectionSize)
            .Select(s => s.ToString()) // some mapping
            .ToList();
        StringsHashSet = Enumerable.Range(1, CollectionSize)
            .Select(s => s.ToString()) // some mapping
            .ToHashSet();
    }

    private List<string> Strings = null!;
    private HashSet<string> StringsHashSet = null!;

    [Benchmark(Baseline = true)]
    public SampleMessage ViaDirectCopy() => new SampleMessage
    {
        Names = { Strings }
    };

    [Benchmark]
    public SampleMessage ViaDirectCopyHashSet() => new SampleMessage
    {
        Names = { StringsHashSet }
    };

    [Benchmark]
    public SampleMessage PreInitSize()
    {
        SampleMessage result = new()
        {
            Names = { Capacity = CollectionSize }
        };
        foreach (var s in Strings)
        {
            result.Names.Add(s);
        }
        return result;
    }
}