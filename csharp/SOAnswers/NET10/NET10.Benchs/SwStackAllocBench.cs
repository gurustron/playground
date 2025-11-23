using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs;

[MemoryDiagnoser(displayGenColumns: false)]
// [DisassemblyDiagnoser]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class SwStackAllocBench
{
    [Benchmark]
    public TimeSpan WithStartNew()
    {
        Stopwatch sw = Stopwatch.StartNew();
        Nop();
        sw.Stop();

        return sw.Elapsed;
    }

    [Benchmark]
    public Stopwatch WithStartNewReturn()
    {
        Stopwatch sw = Stopwatch.StartNew();
        Nop();
        sw.Stop();

        return sw;
    }

    [Benchmark]
    public TimeSpan WithStartNewPass()
    {
        Stopwatch sw = Stopwatch.StartNew();
        Nop(sw);
        sw.Stop();

        return sw.Elapsed;
    }

    [Benchmark]
    public TimeSpan WithStartNewPassInlined()
    {
        Stopwatch sw = Stopwatch.StartNew();
        NopInlined(sw);
        sw.Stop();

        return sw.Elapsed;
    }
    
    [Benchmark]
    public TimeSpan WithStartNewPassByRef()
    {
        Stopwatch sw = Stopwatch.StartNew();
        NopByRef(ref sw);
        sw.Stop();

        return sw.Elapsed;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Nop(){}
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Nop(Stopwatch _) {}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void NopInlined(Stopwatch _) {}

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void NopByRef(ref Stopwatch _) {}
}
