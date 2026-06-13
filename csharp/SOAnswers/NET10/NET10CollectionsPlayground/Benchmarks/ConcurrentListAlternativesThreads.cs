using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

// namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
public class ConcurrentListAlternativesThreads
{
    [Params(8, 4, 2, 1)]
    public int ThreadCount;

    [Params(100_000, 15_000, 100, 15, 1)]
    public int SizePerThread;

    [Benchmark]
    public int[] ProcessViaConcurrentBag()
    {
        var result = new ConcurrentBag<int>();

        var threads = new Thread[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Add(local * SizePerThread + j);
                }
            });
        }

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentQueue()
    {
        var result = new ConcurrentQueue<int>();

        var threads = new Thread[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Enqueue(local * SizePerThread + j);
                }
            });
        }

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentStack()
    {
        var result = new ConcurrentStack<int>();

        var threads = new Thread[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result.Append(local * SizePerThread + j);
                }
            });
        }

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaPreAllocated()
    {
        var result = new int[ThreadCount * SizePerThread];

        var threads = new Thread[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    result[local * SizePerThread + j] = local * SizePerThread + j;
                }
            });
        }

        return result;
    }

    public async Task<int[]> ProcessViaChannelConCurrentRead()
    {
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(ThreadCount * SizePerThread)
        {
            SingleReader = true,
        });
        var result = new int[ThreadCount * SizePerThread];
        
        var writerTask = Task.Run(async () =>
        {
            var indexer = 0;

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                result[indexer++] = item;
            }
        });

        var threads = new Thread[ThreadCount];
        for (int i = 0; i < ThreadCount; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < SizePerThread; j++)
                {
                    channel.Writer.TryWrite(i);
                }
            });
        }

        channel.Writer.Complete();

        await writerTask;

        return result;
    }
}