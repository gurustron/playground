using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
public class GrpcRepeatedField
{
    private static IEnumerable<string> Strings = Enumerable.Repeat("42", 500)
        .Select(s => s.ToString())        ;

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
            Names = { Capacity = 500 }
        };
        foreach (var s in Strings)
        {
            result.Names.Add(s);
        }
        return result;
    }
}