using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

public class RotateVectorBench
{
    public static ulong[] Array { get; } 

    static RotateVectorBench()
    {
        Array = new ulong[Vector<ulong>.Count];
        
    }
    
    [Benchmark]
    public void RotateVector()
    {
        
    }
}

