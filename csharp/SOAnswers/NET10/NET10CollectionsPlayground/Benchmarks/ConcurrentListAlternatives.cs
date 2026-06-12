using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
public class ConcurrentListAlternatives
{
    [Params(1, 2, 4, 8)]
    public int ThreadCount;

    [Params(100_000, 1, 4, 25, 100)]
    public int SizePerThread;

    [Benchmark]
    public int[] ProcessViaConcurrentBag()
    {
        var result = new ConcurrentBag<int>();

        Parallel.For(0, ThreadCount,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Add(i * SizePerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentQueue()
    {
        var result = new ConcurrentQueue<int>();

        Parallel.For(0, ThreadCount,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Enqueue(i * SizePerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentStack()
    {
        var result = new ConcurrentStack<int>();

        Parallel.For(0, ThreadCount,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Append(i * SizePerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaPreAllocated()
    {
        var result = new int[ThreadCount * SizePerThread];

        Parallel.For(0, ThreadCount,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result[i * SizePerThread + j] = i * SizePerThread + j;
                }
            });

        return result;
    }
}