using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace NET7Bench.Benchs;

[MemoryDiagnoser]
public class BigIntModBench
{
    private BigInteger num = BigInteger.Parse("77774737473214712374713471347417134734174712345678912345678" +
                                              "912345678977774737473214712374713471347417134734174712345678912" +
                                              "3456789123456789777747374937849834783477439247192834897329473247" +
                                              "732147123747134713474171347341747123456789123456789123456789");

    [Params(10, 20, 50)] 
    public int A { get; set; }

    public static Dictionary<int, BigInteger> dict = new Dictionary<int, BigInteger>
    {
        { 10, BigInteger.Pow(10, 10) },
        { 20, BigInteger.Pow(10, 20) }, { 50, BigInteger.Pow(10, 50) }
    };

    [Benchmark]
    public BigInteger PowMod()
    {
        BigInteger b2 = BigInteger.Pow(10, A);
        return BigInteger.DivRem(num, b2, out var b3);
    }

    [Benchmark]
    public BigInteger CacheMod()
    {
        BigInteger b2 = dict[A];

        return BigInteger.DivRem(num, b2, out var b3);
    }

    [Benchmark]
    public BigInteger ViaString()
    {
        var s = num.ToString();
        var sLength = Math.Min(s.Length - A - 1, 0);

        return BigInteger.Parse(s.Substring(sLength));
    }

    [Benchmark]
    public string ViaStringReturnString()
    {
        var s = num.ToString();
        var sLength = Math.Min(s.Length - A - 1, 0);
        return s.Substring(sLength);
    }
}
