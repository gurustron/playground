using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

// namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[ThreadingDiagnoser]
public class ConcurrentListAlternatives
{
    [Params(8, 2)]
    public int Threads;

    [Params(10_000, 2500, 25)]
    public int PerThread;

    [Benchmark(Baseline = true)]
    public int[] ProcessViaConcurrentBag()
    {
        var result = new ConcurrentBag<int>();

        Parallel.For(0, Threads,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = Threads,
            },
            i =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Add(i * PerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentQueue()
    {
        var result = new ConcurrentQueue<int>();

        Parallel.For(0, Threads,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = Threads,
            },
            i =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Enqueue(i * PerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentStack()
    {
        var result = new ConcurrentStack<int>();

        Parallel.For(0, Threads,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = Threads,
            },
            i =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Append(i * PerThread + j);
                }
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaPreAllocated()
    {
        var result = new int[Threads * PerThread];

        Parallel.For(0, Threads,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = Threads,
            },
            (i) =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result[i * PerThread + j] = i * PerThread + j;
                }
            });

        return result;
    }
}