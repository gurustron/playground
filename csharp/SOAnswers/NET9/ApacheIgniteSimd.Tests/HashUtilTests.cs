using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace ApacheIgniteSimd.Tests;

public class Tests
{
    private const int R1 = 31;
    private const int R2 = 27;
    private const int R3 = 33;

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

    [TestCase(ulong.MinValue, 1)]  // 0
    [TestCase(ulong.MinValue, 7)]
    [TestCase(ulong.MaxValue, 1)]  // all 1's
    [TestCase(ulong.MaxValue, 13)]
    [TestCase(0UL, 1)]            // zero
    [TestCase(0UL, 31)]
    [TestCase(1UL, 1)]            // one
    [TestCase(1UL, 63)]
    [TestCase(42UL, 1)]           // 42
    [TestCase(42UL, 17)]
    [TestCase(0x123456789ABCDEF0UL, 8)]   // random pattern
    [TestCase(0xFEDCBA9876543210, 16)]  // decreasing pattern
    [TestCase(0xAAAAAAAAAAAAAAAA, 1)]    // alternating pattern
    [TestCase(0x5555555555555555UL, 7)]   // alternating pattern
    public void SingleValue_RotateLeft_ShouldMatchExpected(ulong value, int offset)
    {
        // Arrange
        var expected = BitOperations.RotateLeft(value, offset);

        var ulongs = new ulong[Vector<ulong>.Count];
        for (int i = 0; i < ulongs.Length; i++)
        {
            ulongs[i] = value;
        }
        var vector = new Vector<ulong>(ulongs);
        
        // Act
        var result = PartsForTests.RotateLeft(vector, offset);

        // Assert
        for (int i = 0; i < Vector<ulong>.Count; i++)
        {
            Assert.That(result[i], Is.EqualTo(expected));
        }
    }


    // 

    static IEnumerable<byte[]> Bytes()
    {
        yield return [];
        yield return [1];
        yield return [1, 2, 3, 4];
        yield return [1, 2, 3, 4, 5];
        yield return [byte.MaxValue, byte.MinValue];
        yield return [byte.MaxValue, 1, 2, byte.MinValue];
        yield return [byte.MinValue, 1, 42, byte.MaxValue];
        yield return [byte.MinValue, 1, 42, byte.MaxValue];
        yield return Enumerable.Range(byte.MinValue, byte.MaxValue).Select(b => (byte)b).ToArray();
        ulong[] longs = [
            ulong.MaxValue,
            ulong.MinValue,
            0x123456789ABCDEF0UL,
            0xFEDCBA9876543210,
            0xAAAAAAAAAAAAAAAA,
            0x5555555555555555UL,
        ];

        int[] sizes = [1, 2, 3, 4, 5];

        foreach (var ul in longs)
        foreach (var size in sizes)
        {
            var en = Enumerable.Repeat(ul, size);
            yield return en.SelectMany(BitConverter.GetBytes).ToArray();
            yield return en.SelectMany(BitConverter.GetBytes).Append(byte.MinValue).ToArray();
            yield return en.SelectMany(BitConverter.GetBytes).Append(byte.MaxValue).ToArray();
        }
    }
}