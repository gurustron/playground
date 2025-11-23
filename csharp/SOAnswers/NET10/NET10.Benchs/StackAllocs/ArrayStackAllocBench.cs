using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs.StackAllocs;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class ArrayStackAllocBench
{
    [Benchmark]
    public void ArrayStackAlloc()
    {
        Process(new string[] { "a", "b", "c" });

        static void Process(string[] inputs)
        {
            foreach (string input in inputs)
            {
                Use(input);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Use(string input)
            {
            }
        }
    }
}