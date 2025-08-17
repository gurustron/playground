using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

[MemoryDiagnoser]
public class LinqMaxBench
{
    private decimal[] _array = default!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _array = new decimal[100];
        for (int i = 0; i < _array.Length; i++) _array[i] = (decimal)i;
    }

    [Benchmark]
    public decimal MyMax()
    {
        ReadOnlySpan<decimal> span = _array;
        if (span.IsEmpty) throw new ArgumentException();

        decimal value = span[0];
        for (int i = 1; (uint)i < (uint)span.Length; i++)
        {
            if (span[i] > value)
            {
                value = span[i];
            }
        }

        return value;
    }

    [Benchmark]
    public decimal Linq()
    {
        return _array.Max();
    }
}