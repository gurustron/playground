// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Serilog.Formatting.Compact;

Console.WriteLine("Hello, World!");
// var serilogBenchmark = new SerilogBenchmark();
// serilogBenchmark.TestLogExtension();
// serilogBenchmark.TestLogAction();
BenchmarkRunner.Run<SerilogBenchmark>();