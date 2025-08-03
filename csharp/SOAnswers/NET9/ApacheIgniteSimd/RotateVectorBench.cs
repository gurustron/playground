using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace ApacheIgniteSimd;

public class RotateVectorBench
{
    [Params(0, 1, 16, 31, 32, 64, 65)]
    public int Offset;
    public static ulong[] Array { get; } 

    static RotateVectorBench()
    {
        Span<ulong> sampleData =
        [
            0xAAAAAAAAAAAAAAAA,
            0x123456789ABCDEF0,
            0x42UL,
            0x123456789ABCDEF0UL
        ];

        Array = new ulong[Vector<ulong>.Count];
        
        for (int i = 0; i < Vector<ulong>.Count; i++)
        {
            if (sampleData.Length > i)
            {
                Array[i] = sampleData[i];
            }
            
            Array[i] = ulong.CreateSaturating(i);
        }
        
        

    }
    
    [Benchmark]
    public ulong RotateVector()
    {
        var vector = new Vector<ulong>(Array);
        PartsForTests.RotateLeft(vector, Offset);
        return vector[Vector<ulong>.Count - 1];
    }


    [Benchmark]
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
    
    [Benchmark]
    public ulong RotateUnrolling4()
    {
        BitOperations.RotateLeft(Array[0], Offset);
        BitOperations.RotateLeft(Array[1], Offset);
        BitOperations.RotateLeft(Array[2], Offset);
        return BitOperations.RotateLeft(Array[3], Offset);
    }
}

