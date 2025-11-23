using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using NET10.Benchs;
using NET10.Benchs.Devirtualization;
using NET10.Benchs.StackAllocs;

// dotnet run -c Release -f net9.0 --runtimes net9.0 net10.0
// BenchmarkRunner.Run<GDVBenchs>();

var config = DefaultConfig.Instance
    .AddJob(Job.Default.WithId("ChecksOne"))
    .AddJob(Job.Default.WithId("ChecksThree")
        .WithEnvironmentVariable("DOTNET_JitGuardedDevirtualizationMaxTypeChecks", "3"));
BenchmarkRunner.Run<Tests>(config);

[HideColumns("Error", "StdDev", "Median", "RatioSD", "EnvironmentVariables")]
[DisassemblyDiagnoser]
public class Tests
{
    private readonly A _a = new();
    private readonly B _b = new();
    private readonly C _c = new();

    [Benchmark]
    public void Multiple()
    {
        DoWork(_a);
        DoWork(_b);
        DoWork(_c);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int DoWork(IMyInterface i) => i.GetValue();

    private interface IMyInterface { int GetValue(); }
    private class A : IMyInterface { public int GetValue() => 123; }
    private class B : IMyInterface { public int GetValue() => 456; }
    private class C : IMyInterface { public int GetValue() => 789; }
}