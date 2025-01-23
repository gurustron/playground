using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using BenchmarkDotNet.Attributes;

namespace NET9.Benchs;

[MemoryDiagnoser(displayGenColumns: true)]
public class WaitForTaskWithTimeoutBenchmark
{
    [Params(true, false)]
    public bool IsThrowing;

    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(50);

    [Benchmark]
    public async Task WhenAllDelay()
    {
        var delay = Task.Delay(Timeout);
        var task = GetTask(IsThrowing);
        
        await Task.WhenAny(delay, task);
    }

    [Benchmark]
    public async Task WhenAllDelayReversed()
    {
        var delay = Task.Delay(Timeout);
        var task = GetTask(IsThrowing);
        
        await Task.WhenAny(task, delay);
    }

    [Benchmark]
    public async Task WaitAsyncTimeout()
    {
        try
        {
            await GetTask(IsThrowing).WaitAsync(Timeout);
        }
        catch (TimeoutException)
        {
        }
    }

    private static async Task GetTask(bool exception, [CallerMemberName] string methodName = "")
    {
        await Task.Delay(100);
        if (exception)
        {
            throw new Exception(methodName);
        }
    }
}