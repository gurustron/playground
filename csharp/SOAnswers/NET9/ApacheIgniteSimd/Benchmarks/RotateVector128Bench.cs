using System.Drawing;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;

namespace ApacheIgniteSimd;

public class RotateVector128Bench
{
    [Params(0, 1, 16, 31, 32, 64, 65)]
    public byte Offset;
    public static ulong[] Array { get; }
    public const int Size = 4;

    static RotateVector128Bench()
    {
        Span<ulong> sampleData =
        [
            0xAAAAAAAAAAAAAAAA,
            0x123456789ABCDEF0,
            0x42UL,
            0x123456789ABCDEF0UL
        ];

        Array = new ulong[Size];

        for (int i = 0; i < Size; i++)
        {
            if (sampleData.Length > i)
            {
                Array[i] = sampleData[i];
            }

            Array[i] = ulong.CreateSaturating(i);
        }



    }

    [Benchmark]
    public ulong RotateLeftSse2()
    {
        var vector1 = Vector128.Create(Array[0], Array[1]);
        var vector2 = Vector128.Create(Array[2], Array[3]);
        PartsForTests.RotateLeftSse2(vector1, Offset);
        PartsForTests.RotateLeftSse2(vector2, Offset);
        return vector2[1];
    }

    [Benchmark]
    public ulong RotateLeftSse2Const()
    {
        var vector1 = Vector128.Create(Array[0], Array[1]);
        var vector2 = Vector128.Create(Array[2], Array[3]);
        RotateLeftSse2Const31(vector1);
        RotateLeftSse2Const31(vector2);
        return vector2[1];
    }

    [Benchmark]
    public ulong RotateVector()
    {
        var vector1 = Vector128.Create(Array[0], Array[1]);
        var vector2 = Vector128.Create(Array[2], Array[3]);
        PartsForTests.RotateLeftOperators(vector1, Offset);
        PartsForTests.RotateLeftOperators(vector2, Offset);
        return vector2[1];
    }

    [Benchmark(Baseline = true)]
    public ulong RotateBit()
    {
        var size = Vector<ulong>.Count;
        ulong data = 0;
        for (int i = 0; i < size; i++)
        {
            data = BitOperations.RotateLeft(Array[i], Offset);
        }

        return data;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector128<ulong> RotateLeftSse2Const31(Vector128<ulong> v)
    {
        return
            Sse2.Or(
            Sse2.ShiftLeftLogical(v, 31),
            Sse2.ShiftLeftLogical(v, 64 - 31));
    }
}

