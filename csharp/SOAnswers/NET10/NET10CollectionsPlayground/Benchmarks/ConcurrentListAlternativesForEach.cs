using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

// namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
public class ConcurrentListAlternativesForEach
{
    [Params(8, 4, 2, 1)]
    public int ThreadCount;

    [Params(100_000, 15_000, 100, 1)]
    public int TotalSize;

    [GlobalSetup]
    public void GlobalSetup()
    {
        Collection = Enumerable.Range(1, TotalSize).ToList();
    }

    private List<int> Collection = null!;

    [Benchmark(Baseline = true)]
    public int[] ProcessViaConcurrentBag()
    {
        var result = new ConcurrentBag<int>();

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                Thread.SpinWait(10);
                result.Add(i);
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentQueue()
    {
        var result = new ConcurrentQueue<int>();

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                Thread.SpinWait(10);
                result.Enqueue(i);
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentStack()
    {
        var result = new ConcurrentStack<int>();

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            i =>
            {
                Thread.SpinWait(10);
                result.Append(i);
            });

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaPreAllocated()
    {
        var result = new int[TotalSize];

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            (i, _, index) =>
            {
                Thread.SpinWait(10);
                result[index] = i;
            });

        return result;
    }

    [Benchmark]
    public async Task<int[]> ProcessViaChannel()
    {
        var result = Channel.CreateBounded<int>(new BoundedChannelOptions(TotalSize)
        {
            SingleReader = true,
        });

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            (i) =>
            {
                Thread.SpinWait(10);
                result.Writer.TryWrite(i);
            });

        result.Writer.Complete();
        var arr = new int[result.Reader.Count];
        int indexer = 0;

        await foreach (var item in result.Reader.ReadAllAsync())
        {
            arr[indexer++] = item;
        }

        return arr;
    }

    [Benchmark]
    public async Task<int[]> ProcessViaChannelConCurrentRead()
    {
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(TotalSize)
        {
            SingleReader = true,
        });
        var result = new int[TotalSize];
        
        var writerTask = Task.Run(async () =>
        {
            var indexer = 0;

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                result[indexer++] = item;
            }
        });

        Parallel.ForEach(Collection,
            new ParallelOptions
            {
                MaxDegreeOfParallelism = ThreadCount,
            },
            (i) =>
            {
                Thread.SpinWait(10);
                channel.Writer.TryWrite(i);
            });

        channel.Writer.Complete();

        await writerTask;

        return result;
    }
}