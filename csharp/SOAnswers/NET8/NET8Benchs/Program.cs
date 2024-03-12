using System.Runtime;
using BenchmarkDotNet.Running;
using NET8Benchs;

Console.WriteLine(GCSettings.IsServerGC);
var summary = BenchmarkRunner.Run<QueryTrackingBehaviorBench>();