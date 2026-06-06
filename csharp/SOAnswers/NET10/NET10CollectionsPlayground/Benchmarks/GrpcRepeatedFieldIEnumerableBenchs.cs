using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
public class GrpcRepeatedFieldIEnumerableBenchs
{
    [Params(1, 4, 5, 25, 299)]
    public int CollectionSize;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Strings = Enumerable.Range(1, CollectionSize)
            .Select(s => s.ToString());
    }

    private IEnumerable<string> Strings = null!;

    [Benchmark]
    public SampleMessage ViaDirectCopy() => new SampleMessage
    {
        Names = { Strings }
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