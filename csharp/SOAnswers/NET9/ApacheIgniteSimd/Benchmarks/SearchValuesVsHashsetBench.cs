using System.Drawing;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

[MemoryDiagnoser]
public class SearchValuesVsHashsetBench
{
    static readonly string[] values = ["2019/0002391", "2019/0002390", "2019/0001990"];
    HashSet<string> hs = new(values, StringComparer.Ordinal);
    HashSet<string> hs = new(values, StringComparer.Ordinal);
    SearchValues<string> sv = SearchValues.Create(values.AsSpan(), StringComparison.Ordinal);


    [Benchmark]
    public void UseHashSet()
    {

    }
}

public class CreateCustomerDto
{
    public string? Name { get; set; }
    // ...
}