using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using ProtoTest;

// namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[ThreadingDiagnoser]
public class ConcurrentListAlternativesThreads
{
    [Params(8, 2)]
    public int Threads;

    [Params(10_000, 2500, 25)]
    public int PerThread;

    [Benchmark(Baseline = true)]
    public int[] ProcessViaConcurrentBag()
    {
        var result = new ConcurrentBag<int>();

        var threads = new Thread[Threads];

        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Add(local * PerThread + j);
                }
            });
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }

        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentQueue()
    {
        var result = new ConcurrentQueue<int>();

        var threads = new Thread[Threads];

        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Enqueue(local * PerThread + j);
                }
            });
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }
        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaConcurrentStack()
    {
        var result = new ConcurrentStack<int>();

        var threads = new Thread[Threads];

        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result.Append(local * PerThread + j);
                }
            });
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }
        return result.ToArray();
    }

    [Benchmark]
    public int[] ProcessViaPreAllocated()
    {
        var result = new int[Threads * PerThread];

        var threads = new Thread[Threads];

        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    result[local * PerThread + j] = local * PerThread + j;
                }
            });
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }
        return result;
    }

    [Benchmark]
    public async Task<int[]> ProcessViaChannelConcurrentRead()
    {
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(Threads * PerThread)
        {
            SingleReader = false,
        });
        var result = new int[Threads * PerThread];
        
        var writerTask = Task.Run(async () =>
        {
            var indexer = 0;

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                result[indexer++] = item;
            }
        });

        var threads = new Thread[Threads];
        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    channel.Writer.TryWrite(local * PerThread + j);
                }
            });
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }

        channel.Writer.Complete();

        await writerTask;

        return result;
    }

    [Benchmark]
    public async Task<int[]> ProcessViaChannelSingleReaderConcurrentRead()
    {
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(Threads * PerThread)
        {
            SingleReader = true,
        });
        var result = new int[Threads * PerThread];
        
        var writerTask = Task.Run(async () =>
        {
            var indexer = 0;

            await foreach (var item in channel.Reader.ReadAllAsync())
            {
                result[indexer++] = item;
            }
        });

        var threads = new Thread[Threads];
        for (int i = 0; i < Threads; i++)
        {
            var local = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < PerThread; j++)
                {
                    channel.Writer.TryWrite(local * PerThread + j);
                }
            });
        }
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }

        channel.Writer.Complete();

        await writerTask;

        return result;
    }
}