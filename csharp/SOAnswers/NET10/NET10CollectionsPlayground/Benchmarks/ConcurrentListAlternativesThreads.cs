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
    [Params(8, 2)]
    public int ThreadCount;

    [Params(10_000, 2500, 25)]
    public int SizePerThread;

    [Benchmark(Baseline = true)]
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

    // [Benchmark]
    // public int[] ProcessViaConcurrentQueue()
    // {
    //     var result = new ConcurrentQueue<int>();

    //     var threads = new Thread[ThreadCount];

    //     for (int i = 0; i < ThreadCount; i++)
    //     {
    //         var local = i;
    //         threads[i] = new Thread(() =>
    //         {
    //             for (int j = 0; j < SizePerThread; j++)
    //             {
    //                 result.Enqueue(local * SizePerThread + j);
    //             }
    //         });
    //     }
    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Start();
    //     }

    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Join();
    //     }
    //     return result.ToArray();
    // }

    // [Benchmark]
    // public int[] ProcessViaConcurrentStack()
    // {
    //     var result = new ConcurrentStack<int>();

    //     var threads = new Thread[ThreadCount];

    //     for (int i = 0; i < ThreadCount; i++)
    //     {
    //         var local = i;
    //         threads[i] = new Thread(() =>
    //         {
    //             for (int j = 0; j < SizePerThread; j++)
    //             {
    //                 result.Append(local * SizePerThread + j);
    //             }
    //         });
    //     }
    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Start();
    //     }

    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Join();
    //     }
    //     return result.ToArray();
    // }

    // [Benchmark]
    // public int[] ProcessViaPreAllocated()
    // {
    //     var result = new int[ThreadCount * SizePerThread];

    //     var threads = new Thread[ThreadCount];

    //     for (int i = 0; i < ThreadCount; i++)
    //     {
    //         var local = i;
    //         threads[i] = new Thread(() =>
    //         {
    //             for (int j = 0; j < SizePerThread; j++)
    //             {
    //                 result[local * SizePerThread + j] = local * SizePerThread + j;
    //             }
    //         });
    //     }
    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Start();
    //     }

    //     for (int i = 0; i < threads.Length; i++)
    //     {
    //         threads[i].Join();
    //     }
    //     return result;
    // }

    [Benchmark]
    public async Task<int[]> ProcessViaChannelConcurrentRead()
    {
        var channel = Channel.CreateBounded<int>(new BoundedChannelOptions(ThreadCount * SizePerThread)
        {
            // SingleReader = true,
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
                    channel.Writer.TryWrite(local * SizePerThread + j);
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