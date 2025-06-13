using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

[MemoryDiagnoser(displayGenColumns: true)]
public class BigIntToIntBench
{
    private static int[] BitSizes = [32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536];

    private static BigInteger[] BigIntegers = BitSizes
        .Select(bits => BigInteger.One << (bits - 1))
        .SelectMany(b => new[] { b, -b })
        .ToArray();

    [Benchmark]
    public void MaskToInt()
    {
        foreach (var bi in BigIntegers)
        {
            ViaMaskToInt(bi);
        }
    }

    [Benchmark]
    public void ToInt()
    {
        foreach (var bi in BigIntegers)
        {
            bi.ToInt();
        }
    }

    [Benchmark]
    public void ToInt32Truncate()
    {
        foreach (var bi in BigIntegers)
        {
            bi.ToInt32Truncate();
        }
    }

    [Benchmark]
    public void ToInt32TruncateCompiled()
    {
        foreach (var bi in BigIntegers)
        {
            bi.ToInt32TruncateCompiled();
        }
    }

    [Benchmark]
    public void ToInt32TruncateUnsafeAccessor()
    {
        foreach (var bi in BigIntegers)
        {
            bi.ToInt32TruncateUnsafeAccessor();
        }
    }
    
    [Benchmark]
    public void GenericMath()
    {
        foreach (var bi in BigIntegers)
        {
            ViaGenericMath(bi);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int ViaMaskToInt(BigInteger big) => unchecked((int)(uint)(big & uint.MaxValue));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int ViaGenericMath(BigInteger big) => int.CreateTruncating(big);
}

public static class BigIntegerToByteArrayExtensions
{
    public static int ToInt(this BigInteger big)
    {
        return BitConverter.ToInt32(big.ToByteArray().AsSpan(0, 4));
    }
}

public static class BigIntegerExtensions
{
    private static readonly FieldInfo s_signField;
    private static readonly FieldInfo s_bitsField;

    static BigIntegerExtensions()
    {
        s_signField = typeof(BigInteger)
            .GetField("_sign", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new Exception("Could not find BigInteger._sign field");

        s_bitsField = typeof(BigInteger)
            .GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new Exception("Could not find BigInteger._bits field");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ToUInt32Truncate(this BigInteger value)
    {
        if (value.GetBitLength() < 256)
        {
            return BitConverter.ToUInt32(value.ToByteArray().AsSpan(0, 4));
        }

        uint[]? magnitude = (uint[]?)s_bitsField.GetValue(value);
        int sign = (int)s_signField.GetValue(value)!;

        if (magnitude is null)
        {
            if (sign >= 0)
                return (uint)sign;

            unchecked
            {
                uint absSign = (uint)(-sign);
                return (uint)(~absSign + 1);
            }
        }

        uint lowWord = magnitude.Length > 0 ? magnitude[0] : 0u;
        return sign > 0 ? lowWord : unchecked((uint)(~lowWord + 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32Truncate(this BigInteger value)
    {
        uint u = value.ToUInt32Truncate();
        return unchecked((int)u);
    }
}

public static class BigIntegerExtensionsUnsafeAccessor
{
    // internal readonly int _sign; // Do not rename (binary serialization)
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_sign")]
    extern static ref int GetSetSign(ref BigInteger c);

    // internal readonly uint[]? _bits; // Do not rename (binary serialization)
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_bits")]
    extern static ref uint[]? GetSetBits(ref BigInteger c);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ToUInt32TruncateUnsafeAccessor(this BigInteger value)
    {
        if (value.IsZero) return 0u;

        int sign = GetSetSign(ref value);
        uint[]? magnitude = GetSetBits(ref value);

        if (magnitude is null)
        {
            if (sign >= 0) return (uint)sign;
            unchecked
            {
                uint absSign = (uint)(-sign);
                return (uint)(~absSign + 1);
            }
        }

        uint lowWord = magnitude.Length > 0 ? magnitude[0] : 0u;
        return sign > 0 ? lowWord : unchecked((uint)(~lowWord + 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32TruncateUnsafeAccessor(this BigInteger value)
    {
        return unchecked((int)value.ToUInt32TruncateUnsafeAccessor());
    }
}

public static class BigIntegerCompiledAccess
{
    public static readonly Func<BigInteger, int> GetSign;
    public static readonly Func<BigInteger, uint[]?> GetBits;

    static BigIntegerCompiledAccess()
    {
        // Expression: (BigInteger x) => x._sign
        var param = Expression.Parameter(typeof(BigInteger), "x");

        var signField = typeof(BigInteger)
            .GetField("_sign", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException("Field '_sign' not found");

        var bitsField = typeof(BigInteger)
            .GetField("_bits", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException("Field '_bits' not found");

        GetSign = Expression.Lambda<Func<BigInteger, int>>(
            Expression.Field(param, signField), param).Compile();

        GetBits = Expression.Lambda<Func<BigInteger, uint[]?>>(
            Expression.Field(param, bitsField), param).Compile();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ToUInt32TruncateCompiled(this BigInteger value)
    {
        if (value.IsZero) return 0u;

        int sign = BigIntegerCompiledAccess.GetSign(value);
        uint[]? magnitude = BigIntegerCompiledAccess.GetBits(value);

        if (magnitude is null)
        {
            if (sign >= 0) return (uint)sign;
            unchecked
            {
                uint absSign = (uint)(-sign);
                return (uint)(~absSign + 1);
            }
        }

        uint lowWord = magnitude.Length > 0 ? magnitude[0] : 0u;
        return sign > 0 ? lowWord : unchecked((uint)(~lowWord + 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32TruncateCompiled(this BigInteger value)
    {
        return unchecked((int)value.ToUInt32TruncateCompiled());
    }
}