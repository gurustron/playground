using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using static NET10.Benchs.OuterClass;

namespace NET10.Benchs;

public class MoveToClassBench
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Do() => Enumerable.Range(0, 100).Sum();
    
    [Benchmark] 
    public int InOuterClass() => Method1();

    [Benchmark] 
    public int InClass() => Do();   
}

