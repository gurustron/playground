
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using ApacheIgniteSimd;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Console.WriteLine("Hello, World!");
// Console.WriteLine(Vector128.IsHardwareAccelerated);
// Console.WriteLine(Vector128<ulong>.Count);
// var r = Sse2.ShiftLeftLogical(
//     Vector128.Create((ulong)1, 1),
//     Vector128.Create((ulong)1, 4));
// Console.WriteLine(r);
// Console.WriteLine(r[0].ToString("x8"));
// Console.WriteLine(r[1].ToString("x8"));
// unchecked
// {
//     var getU = () => ulong.MaxValue;
//     Console.WriteLine(getU() * 0x87c37b91114253d5L);
// }

// Vector<ulong> v = new Vector<ulong>(new []{ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue});

// {
//     var vector = v * 0x87c37b91114253d5L;
//     Console.WriteLine(vector[0]);
// }
var config = DefaultConfig.Instance
    .AddJob(Job.Default.WithId("Scalar")
        .WithEnvironmentVariable("DOTNET_EnableHWIntrinsic", "0")
        .AsBaseline())
    .AddJob(Job.Default.WithId("Vector128")
        .WithEnvironmentVariable("DOTNET_EnableAVX2", "0")
        .WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"))
    // .AddJob(Job.Default.WithId("Vector256").WithEnvironmentVariable("DOTNET_EnableAVX512F", "0"))
    .AddJob(Job.Default.WithId("Vector512"))
    ;
// BenchmarkRunner.Run<CalculateHashBench>(config);
BenchmarkRunner.Run<CalculateHashBench>();

