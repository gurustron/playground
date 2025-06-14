namespace ApacheIgniteSimd.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(Bytes))]
    public void Hash64Internal(byte[] data)
    {
        ReadOnlySpan<byte> readOnlySpan = data.AsSpan();
        ulong seed = 1;
        var simd = HashUtilsSimd.Hash64Internal(readOnlySpan, seed);
        var baseline = HashUtilsBaseline.Hash64Internal(readOnlySpan, seed);
        Assert.That(simd, Is.EqualTo(baseline));
    }


    static IEnumerable<byte[]> Bytes()
    {
        yield return [];
        yield return [1];
        yield return [1, 2, 3, 4];
        yield return [byte.MaxValue, byte.MinValue];
        yield return [byte.MaxValue, 1, 2, byte.MinValue];
        yield return [byte.MinValue, 1, 42, byte.MaxValue];
        yield return [byte.MinValue, 1, 42, byte.MaxValue];
        yield return Enumerable.Range(byte.MinValue, byte.MaxValue).Select(b => (byte)b).ToArray();
    }
}