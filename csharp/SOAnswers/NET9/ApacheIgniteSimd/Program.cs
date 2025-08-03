
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using ApacheIgniteSimd;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");
Console.WriteLine(Vector128.IsHardwareAccelerated);
Console.WriteLine(Vector128<ulong>.Count);
var r = Sse2.ShiftLeftLogical(
    Vector128.Create((ulong)1, 1),
    Vector128.Create((ulong)1, 4));
Console.WriteLine(r);
Console.WriteLine(r[0].ToString("x8"));
Console.WriteLine(r[1].ToString("x8"));
unchecked
{
    var getU = () => ulong.MaxValue;
    Console.WriteLine(getU() * 0x87c37b91114253d5L);
}

Vector<ulong> v = new Vector<ulong>(new []{ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue});

{
    var vector = v * 0x87c37b91114253d5L;
    Console.WriteLine(vector[0]);
}
// BenchmarkRunner.Run<RotateVectorBench>();

