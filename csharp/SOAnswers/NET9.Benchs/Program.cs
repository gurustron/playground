using System.Diagnostics;
using BenchmarkDotNet.Running;
using NET9.Benchs;

Console.WriteLine("Hello, World!");
// var serilogBenchmark = new SerilogBenchmark();
// serilogBenchmark.TestLogExtension();
// serilogBenchmark.TestLogAction();
// BenchmarkRunner.Run<SerilogBenchmark>();

// TaskScheduler.UnobservedTaskException += (s, e) => { };
// BenchmarkRunner.Run<WaitForTaskWithTimeoutBenchmark>();

await MeasureAllocs();
Environment.Exit(0);

var wait = new WaitForTaskWithTimeoutBenchmark
{
    IsThrowing = true,
    WaitOriginal = false
};

var actions = new []
{
    () => (nameof(wait.WhenAllDelay), wait.WhenAllDelay()),
    () => (nameof(wait.WhenAllDelayContinueWith), wait.WhenAllDelayContinueWith()),
    () => (nameof(wait.WhenAllDelayReversed), wait.WhenAllDelayReversed()),
    () => (nameof(wait.WaitAsyncTimeout), wait.WaitAsyncTimeout()),
    () => (nameof(wait.WaitAsyncTimeout), wait.WaitAsyncTimeout()), // the last one can be captured by JIT until the end of app
};
AppDomain.CurrentDomain.UnhandledException 
    += (sender, args) => Console.WriteLine($"AppDomain: {sender?.GetType().Name}: {args.ExceptionObject}");
TaskScheduler.UnobservedTaskException += (sender, args)
    => Console.WriteLine($"TaskScheduler: {sender?.GetType().Name}: {args.Exception?.Message}");
foreach (var func in actions)
{
    var (name, task) = func();
    Console.WriteLine($"Running: {name}");
    await task;
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();    
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();
}

for (int i = 0; i < 4; i++)
{
    Thread.Sleep(100);
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();
}

async Task MeasureAllocs()
{
    var localInstance = new WaitForTaskWithTimeoutBenchmark
    {
        IsThrowing = true,
        WaitOriginal = false
    };
    
    Console.WriteLine($"WhenAllDelay {await Measure(localInstance.WhenAllDelay)}");
    Console.WriteLine($"WhenAllDelayContinueWith {await Measure(localInstance.WhenAllDelayContinueWith)}");
    Console.WriteLine($"WhenAllDelayReversed {await Measure(localInstance.WhenAllDelayReversed)}");
    Console.WriteLine($"WaitAsyncTimeout {await Measure(localInstance.WaitAsyncTimeout)}");

    Console.WriteLine("Not throwing");
    localInstance.IsThrowing = false;

    Console.WriteLine($"WhenAllDelay {await Measure(localInstance.WhenAllDelay)}");
    Console.WriteLine($"WhenAllDelayContinueWith {await Measure(localInstance.WhenAllDelayContinueWith)}");
    Console.WriteLine($"WhenAllDelayReversed {await Measure(localInstance.WhenAllDelayReversed)}");
    Console.WriteLine($"WaitAsyncTimeout {await Measure(localInstance.WaitAsyncTimeout)}");

    async Task<long> Measure(Func<Task> t,int iterations = 100)
    {
        for (int i = 0; i < 5; i++)
        {
            await t();
            _ = GC.GetTotalAllocatedBytes(true);
        }

        var before = GC.GetTotalAllocatedBytes(true);
        for (int i = 0; i < iterations; i++)
        {
            await t();
        }
        return GC.GetTotalAllocatedBytes(true) - before;
    }
}