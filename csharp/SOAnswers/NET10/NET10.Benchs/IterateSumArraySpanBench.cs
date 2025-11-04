using BenchmarkDotNet.Attributes;

namespace NET10.Benchs;

[DisassemblyDiagnoser(printSource: true)]
public class IterateSumArraySpanBench
{
    private static readonly int[] Array = Enumerable.Range(0, 10_000_001).ToArray();

    [Benchmark]
    public int Span()
    {
        var sum = 0;
        ReadOnlySpan<int> span = Array;
        for (int i = 0; i < span.Length; i++)
        {
            sum += span[i];
        }
        return sum;
    }

    [Benchmark]
    public int Arr()
    {
        var sum = 0;
        int[] arr = Array;
        for (int i = 0; i < arr.Length; i++)
        {
            sum += arr[i];
        }
        return sum;
    }
    
    [Benchmark]
    public int ArrNoLocalCopy()
    {
        var sum = 0;
        for (int i = 0; i < Array.Length; i++)
        {
            sum += Array[i];
        }
        return sum;
    }
}