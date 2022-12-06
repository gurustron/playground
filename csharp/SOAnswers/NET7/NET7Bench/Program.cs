// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using NET7Bench.Benchs;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<RegexBenchs>();