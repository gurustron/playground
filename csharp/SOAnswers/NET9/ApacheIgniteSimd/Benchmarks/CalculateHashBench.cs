using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

[MemoryDiagnoser]
public class CalculateHashBench
{
    [Params(1, 5, 10, 100, 1000)]
    public int N;

    private byte[] data = null!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        data = new byte[N]; // executed once per each N value
        Array.Fill(data, byte.MaxValue);
    }

    // [Benchmark]
    // public ulong CalculateHashV128() => HashUtilsSimd.Hash64InternalVector128(data, 42L);

    [Benchmark]
    public ulong Hash64InternalUnrolled() => HashUtilsSimd.Hash64InternalUnrolled(data, 42L);

    [Benchmark(Baseline = true)]
    public ulong CalculateHashBaseline() => HashUtilsBaseline.Hash64Internal(data, 42L);
}