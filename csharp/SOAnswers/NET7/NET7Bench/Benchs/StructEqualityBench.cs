using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using BenchmarkDotNet.Attributes;

namespace NET7Bench.Benchs;

public class StructEqualityBench
{
    private const int iterations = 10_000;

    private Data left = new()
    {
        b7 = 1
    };

    private Data right = new()
    {
        b7 = 1
    };

    [Benchmark]
    public bool AutoEquals()
    {
        var r = true;
        for (int i = 0; i < iterations; i++)
        {
            r |= left.Equals(right);
        }

        return r;
    }

    [Benchmark]
    public bool ViaCustomEquals()
    {
        var r = true;
        for (int i = 0; i < iterations; i++)
        {
            r |= UnmanagedExtensions.CustomEquals(ref left, ref right);
        }

        return r;
    }

    [Benchmark]
    public bool ViaVector()
    {
        var r = true;
        for (int i = 0; i < iterations; i++)
        {
            r |= UnmanagedExtensions.ViaVector(ref Unsafe.As<Data, byte>(ref left), ref Unsafe.As<Data, byte>(ref right), (uint)Unsafe.SizeOf<Data>());
        }

        return r;
    }

    [Benchmark]
    public bool ViaDataHelper()
    {
        var r = true;
        for (int i = 0; i < iterations; i++)
        {
            var helperThis = Unsafe.As<Data, DataHelper>(ref left);
            var helperOther = Unsafe.As<Data, DataHelper>(ref right);
            r |= helperOther.Equals(helperThis);
        }

        return r;
    }
}

public static class UnmanagedExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CustomEquals<T>(ref T x, ref T y) where T : unmanaged
    {
        var byteSpanX = MemoryMarshal.Cast<T, byte>(MemoryMarshal.CreateReadOnlySpan(ref x, 1));
        var byteSpanY = MemoryMarshal.Cast<T, byte>(MemoryMarshal.CreateReadOnlySpan(ref y, 1));
        return byteSpanX.SequenceEqual(byteSpanY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ViaVector(ref byte first, ref byte second, uint length)
    {
        nuint offset = 0;
        nuint lengthToExamine = length - (nuint)Vector64<byte>.Count;
        if (lengthToExamine != 0)
        {
            do
            {
                if (Vector64.LoadUnsafe(ref first, offset) != Vector64.LoadUnsafe(ref second, offset))
                {
                    return false;
                }

                offset += (nuint)Vector64<byte>.Count;
            } while (lengthToExamine > offset);
        }

        if (Vector64.LoadUnsafe(ref first, lengthToExamine) == Vector64.LoadUnsafe(ref second, lengthToExamine))
        {
            return true;
        }

        return false;
    }
}

public struct Data
{
    public byte b0;
    public byte b1;
    public byte b2;
    public byte b3;
    public byte b4;
    public byte b5;
    public byte b6;
    public byte b7;
}

public struct DataHelper
{
    public long l0;

    public bool Equals(DataHelper other)
    {
        return l0 == other.l0;
    }
}