using BenchmarkDotNet.Attributes;

namespace NET10.Benchs;

public class IterateArraySpanBench
{
    public static readonly int[] Array = Enumerable.Range(0, 10_000_001).ToArray();

    [Benchmark]
    public int Span()
    {
        int num = -1;
        ReadOnlySpan<int> span = Array;
        for (int i = 0; i < span.Length; i++)
        {
            if (span[i] == -1)
                return span[i];
        }
        return num;
    }

    [Benchmark]
    public int Arr()
    {
        int num = -1;
        int[] arr = Array;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == -1)
                return arr[i];
        }
        return num;
    }

}