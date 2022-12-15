using System.Diagnostics.Metrics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace NET7Console.MemoryCacheStats;

public class MemoryCacheStatsPlay
{
    static Meter s_meter = new Meter("Microsoft.Extensions.Caching.Memory.MemoryCache", "1.0.0");
    public static void Do()
    {
        var memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions
        {
            TrackStatistics = true,
        }));
        
        s_meter.CreateObservableGauge("cache-hits", GetCacheHits);
        s_meter.CreateObservableGauge("cache-misses", GetCacheMisses);

        memoryCache.GetOrCreate("1", _ => "1");
        memoryCache.GetOrCreate("1", _ => "1");
        memoryCache.GetOrCreate("2", _ => "2");
        
        memoryCache.TryGetValue("1", out _);
        memoryCache.TryGetValue("2", out _);
        memoryCache.TryGetValue("42", out _);
        memoryCache.TryGetValue("777", out _);

        var cancellationTokenSource = new CancellationTokenSource();

        var cancellationToken = cancellationTokenSource.Token;
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.WriteLine("Press any key to continue");
        var startNew = Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    Spin(i++);

                    void Spin(int iteration)
                    {
                        var (_, top) = Console.GetCursorPosition();
                        Console.SetCursorPosition(0, top);
                        var symbol = (iteration % 3) switch {
                            0 => "|",
                            1 => "/",
                            2 => "-",
                            _ => "\\"
                        };
                        Console.Write(symbol);
                    }

                    Thread.Sleep(250);
                    memoryCache.TryGetValue("1", out _);
                    memoryCache.TryGetValue("42", out _);
                }
            },
            cancellationToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);
        
        Console.ReadKey();
        cancellationTokenSource.Cancel();


        IEnumerable<Measurement<long>> GetCacheHits()
        {
            return new Measurement<long>[]
            {
                new(memoryCache!.GetCurrentStatistics()!.TotalHits, new KeyValuePair<string,object?>("CacheName", "mc1")),
            };
        }
        
        IEnumerable<Measurement<long>> GetCacheMisses()
        {
            return new Measurement<long>[]
            {
                new(memoryCache!.GetCurrentStatistics()!.TotalMisses, new KeyValuePair<string,object?>("CacheName", "mc1")),
            };
        }
    }
}