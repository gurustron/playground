using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using BenchmarkDotNet.Attributes;

namespace NET9.Benchs;

[MemoryDiagnoser(displayGenColumns: true)]
public class WaitForTaskWithTimeoutBenchmark
{
    [Params(true, false)]
    public bool IsThrowing;

    public bool WaitOriginal;

    private static readonly TimeSpan Timeout = TimeSpan.FromMilliseconds(50);

    [Benchmark]
    public async Task WhenAllDelay()
    {
        var delay = Task.Delay(Timeout);
        var task = GetTask(IsThrowing);
        
        await Task.WhenAny(delay, task);
        await SafeWaitOriginalIfConfigured(task);
    }    
    
    [Benchmark]
    public async Task WhenAllDelayContinueWith()
    {
        var delay = Task.Delay(Timeout);
        var task = GetTask(IsThrowing);
        _ = task.ContinueWith(t => { var x = t.Exception;}, // touch the exception!
            CancellationToken.None,
            TaskContinuationOptions.OnlyOnFaulted, 
            TaskScheduler.Default);
        await Task.WhenAny(delay, task);
        await SafeWaitOriginalIfConfigured(task);
    }

    [Benchmark]
    public async Task WhenAllDelayReversed()
    {
        var delay = Task.Delay(Timeout);
        var task = GetTask(IsThrowing);
        
        await Task.WhenAny(task, delay);
        await SafeWaitOriginalIfConfigured(task);
    }

    [Benchmark]
    public async Task WaitAsyncTimeout()
    {
        var task = GetTask(IsThrowing);
        try
        {
            await task.WaitAsync(Timeout);
        }
        catch (TimeoutException)
        {
        }

        await SafeWaitOriginalIfConfigured(task);
    }

    private static async Task GetTask(bool exception, [CallerMemberName] string methodName = "")
    {
        await Task.Delay(100);
        if (exception)
        {
            throw new Exception(methodName);
        }
    }

    private async ValueTask SafeWaitOriginalIfConfigured(Task task)
    {
        if (WaitOriginal)
        {
            try
            {
                await task;

            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}