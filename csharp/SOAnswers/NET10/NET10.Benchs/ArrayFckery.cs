using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace NET10.Benchs;

[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
[HardwareCounters(BenchmarkDotNet.Diagnosers.HardwareCounter.CacheMisses)]
public class ArrayFckery
{
    // Source - https://stackoverflow.com/q/79832351
// Posted by eternity, modified by community. See post 'Timeline' for change history
// Retrieved 2025-11-29, License - CC BY-SA 4.0

    private const int Size = 10000;

    [Benchmark(Baseline = true)]
    public void ByRow()
    {
        var a = new int[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                a[i, j] = 1;
            }
        }
    }

    [Benchmark]
    public void ByColumn()
    {
        var a = new int[Size, Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                a[j, i] = 1;
            }
        }
    }
}