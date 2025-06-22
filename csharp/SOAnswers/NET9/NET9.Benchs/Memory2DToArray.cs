using BenchmarkDotNet.Attributes;
using CommunityToolkit.HighPerformance;

namespace NET9.Benchs;

[MemoryDiagnoser(displayGenColumns: true)]
public class Memory2DToArrayBench
{
    public readonly static Memory2D<int> Mem = new [,]
    {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 }
    };
    
    [Benchmark]
    public int[] ToArray1D() => Mem.ToArray1D();
    
    [Benchmark]
    public int[] ToArray1DViaSpan() => Mem.ToArray1DViaSpan();  

    [Benchmark]
    public int[] ToArray1DViaMemory() => Mem.ToArray1DViaMemory();
}

public static class MemoryExtensions
{
    public static T [] ToArray1D<T>(this Memory2D<T> memory) =>
        ((ReadOnlyMemory2D<T>)memory).ToArray1D();
    
    public static T [] ToArray1D<T>(this ReadOnlyMemory2D<T> memory)
    {
        var array = new T[memory.Length];
        memory.CopyTo(array);
        return array;
    }
    
    public static T [] ToArray1DViaSpan<T>(this Memory2D<T> memory)
    {
        var array = new T[memory.Length];
        memory.Span.CopyTo(array);
        return array;
    }
    
    public static T [] ToArray1DViaMemory<T>(this Memory2D<T> memory)
    {
        var array = new T[memory.Length];
        memory.CopyTo(array);
        return array;
    }
}