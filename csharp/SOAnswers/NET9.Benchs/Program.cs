using BenchmarkDotNet.Running;
using NET9.Benchs;

Console.WriteLine("Hello, World!");
// var serilogBenchmark = new SerilogBenchmark();
// serilogBenchmark.TestLogExtension();
// serilogBenchmark.TestLogAction();
// BenchmarkRunner.Run<SerilogBenchmark>();

// TaskScheduler.UnobservedTaskException += (s, e) => { };
// BenchmarkRunner.Run<WaitForTaskWithTimeoutBenchmark>();

var wait = new WaitForTaskWithTimeoutBenchmark
{
    IsThrowing = true
};

var actions = new []
{
    () => (nameof(wait.WhenAllDelay), wait.WhenAllDelay()),
    () => (nameof(wait.WhenAllDelayReversed), wait.WhenAllDelayReversed()),
    () => (nameof(wait.WaitAsyncTimeout), wait.WaitAsyncTimeout()),
    () => (nameof(wait.WaitAsyncTimeout), wait.WaitAsyncTimeout()), // the last one can be captured by JIT until the end of app
};
TaskScheduler.UnobservedTaskException += (sender, args)
    => Console.WriteLine($"{sender?.GetType().FullName}: {args.Exception?.Message}");
foreach (var func in actions)
{
    var (name, task) = func();
    Console.WriteLine($"Running: {name}");
    await task;
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();
}

for (int i = 0; i < 4; i++)
{
    Thread.Sleep(100);
    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
    GC.WaitForPendingFinalizers();
}
