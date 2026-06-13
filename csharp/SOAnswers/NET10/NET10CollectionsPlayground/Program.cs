using System.Buffers;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using Helpers;

ThreadPool.SetMinThreads(32, 32);
await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Delay(100)));
// ConcurrentListAlternativesForEach concurrentListAlternatives = new ConcurrentListAlternativesForEach();
// concurrentListAlternatives.ThreadCount = 3;
// concurrentListAlternatives.TotalSize = 17;
// concurrentListAlternatives.GlobalSetup();
// System.Console.WriteLine(concurrentListAlternatives.ProcessViaConcurrentBag().Order().ToPrintVersion());
// System.Console.WriteLine((await concurrentListAlternatives.ProcessViaChannelConCurrentRead()).Order().ToPrintVersion());
// System.Console.WriteLine(concurrentListAlternatives.ProcessViaPreAllocated().Order().ToPrintVersion());
BenchmarkRunner.Run<ConcurrentListAlternatives>();
BenchmarkRunner.Run<ConcurrentListAlternativesForEach>();
BenchmarkRunner.Run<ConcurrentListAlternativesThreads>();




// Ignore for now

// var sv = SearchValues.Create(["ab", "cd"], StringComparison.Ordinal);

// Console.WriteLine("qwerrt".IndexOfAny(sv)); // -1 
// Console.WriteLine("qw_ab_rrt".IndexOfAny(sv)); // 3
// Console.WriteLine("qw_cd_rrt".IndexOfAny(sv)); // 3
// Console.WriteLine("qw_cd_rr_ab_t".IndexOfAny(sv)); // 3

// Console.WriteLine(new[]{"qwerrt", "_ab_"}.IndexOfAny(sv)); // -1 
// Console.WriteLine(new[]{"qwerrt", "_ab_", "ab"}.IndexOfAny(sv)); // 2 
// Console.WriteLine(new[]{"qwerrt", "_ab_", "cd"}.IndexOfAny(sv)); // 2 

// List<MyButton> buttons = new(){new(){MyEvent = ""},new(){MyEvent = ""}};
// Dictionary<string, int> stats = new Dictionary<string, int>();

// foreach (var button in  buttons)
// {
//     ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(stats, button.MyEvent, out _);

//     value++;
// }

// System.Console.WriteLine(stats[""]);

// class MyButton
// {
//     public required string MyEvent { get; set; }
// }
