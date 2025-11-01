using BenchmarkDotNet.Running;
using NET10.Benchs;

// dotnet run -c Release -f net9.0 --runtimes net9.0 net10.0
BenchmarkRunner.Run<IterateArraySpanBench>();
