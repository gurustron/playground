using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace ApacheIgniteSimd;

public class HashUtilsSimd
{
    private const ulong C1 = 0x87c37b91114253d5L;
    private const ulong C2 = 0x4cf5ad432745937fL;
    private const int R1 = 31;
    private const int R2 = 27;
    private const int R3 = 33;
    private const int M = 5;
    private const int N1 = 0x52dce729;
    private const int N2 = 0x38495ab5;

    public static int Hash32Internal(ReadOnlySpan<byte> data, ulong seed)
    {
        var hash64 = Hash64InternalVector128(data, seed);

        return (int)(hash64 ^ (hash64 >> 32));
    }

    public static ulong Hash64InternalVector128(ReadOnlySpan<byte> data, ulong seed)
    {
        unchecked
        {
            ulong h1 = seed;
            ulong h2 = seed;
            var length = data.Length;
            int nblocks = length >> 4;

            // body
            if (Vector128.IsHardwareAccelerated)
            {
                Vector128<ulong> firstMul = Vector128.Create(C1, C2);
                Vector128<ulong> secondMul = Vector128.Create(C2, C1);

                // process vector blocks
                for (int i = 0; i < nblocks; i++)
                {
                    int idx = (i << 4);
                    var ks = Vector128.Create(
                        BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx)),
                        BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 8)));

                    ks *= firstMul;

                    ks = Vector128.Create(
                         BitOperations.RotateLeft(ks[0], R1),
                         BitOperations.RotateLeft(ks[1], R3)
                    );

                    ks *= secondMul;
                    tmpH1 = BitOperations.RotateLeft(tmpH1, R2);
                    tmpH1 += originalH2;
                    tmpH1 = tmpH1 * M + N1;

                    var tmpH2 = hs[1];
                    tmpH2 = BitOperations.RotateLeft(tmpH2, R1);
                    tmpH2 += tmpH1;
                    tmpH2 = tmpH2 * M + N2;

                    hs = Vector128.Create(tmpH1, tmpH2);
                }

                h1 = hs[0];
                h2 = hs[1];
            }
            else
            {
                for (int i = 0; i < nblocks; i++)
                {
                    int idx = (i << 4);
                    ulong kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx));
                    ulong kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 8));

                    // mix functions for k1
                    kk1 *= C1;
                    kk1 = BitOperations.RotateLeft(kk1, R1);
                    kk1 *= C2;
                    h1 ^= kk1;
                    h1 = BitOperations.RotateLeft(h1, R2);
                    h1 += h2;
                    h1 = h1 * M + N1;

                    // mix functions for k2
                    kk2 *= C2;
                    kk2 = BitOperations.RotateLeft(kk2, R3);
                    kk2 *= C1;
                    h2 ^= kk2;
                    h2 = BitOperations.RotateLeft(h2, R1);
                    h2 += h1;
                    h2 = h2 * M + N2;
                }
            }

            // tail
            ulong k1 = 0;
            ulong k2 = 0;
            int index = nblocks << 4;
            switch (length - index)
            {
                case 15:
                    k2 ^= ((ulong)data[index + 14] & 0xff) << 48;
                    goto case 14;
                case 14:
                    k2 ^= ((ulong)data[index + 13] & 0xff) << 40;
                    goto case 13;
                case 13:
                    k2 ^= ((ulong)data[index + 12] & 0xff) << 32;
                    goto case 12;
                case 12:
                    k2 ^= ((ulong)data[index + 11] & 0xff) << 24;
                    goto case 11;
                case 11:
                    k2 ^= ((ulong)data[index + 10] & 0xff) << 16;
                    goto case 10;
                case 10:
                    k2 ^= ((ulong)data[index + 9] & 0xff) << 8;
                    goto case 9;

                case 9:
                    k2 ^= (ulong)data[index + 8] & 0xff;
                    k2 *= C2;
                    k2 = BitOperations.RotateLeft(k2, R3);
                    k2 *= C1;
                    h2 ^= k2;
                    goto case 8;

                case 8:
                    k1 ^= ((ulong)data[index + 7] & 0xff) << 56;
                    goto case 7;
                case 7:
                    k1 ^= ((ulong)data[index + 6] & 0xff) << 48;
                    goto case 6;
                case 6:
                    k1 ^= ((ulong)data[index + 5] & 0xff) << 40;
                    goto case 5;
                case 5:
                    k1 ^= ((ulong)data[index + 4] & 0xff) << 32;
                    goto case 4;
                case 4:
                    k1 ^= ((ulong)data[index + 3] & 0xff) << 24;
                    goto case 3;
                case 3:
                    k1 ^= ((ulong)data[index + 2] & 0xff) << 16;
                    goto case 2;
                case 2:
                    k1 ^= ((ulong)data[index + 1] & 0xff) << 8;
                    goto case 1;

                case 1:
                    k1 ^= (ulong)data[index] & 0xff;
                    k1 *= C1;
                    k1 = BitOperations.RotateLeft(k1, R1);
                    k1 *= C2;
                    h1 ^= k1;
                    break;
            }

            // finalization
            h1 ^= (ulong)length;
            h2 ^= (ulong)length;

            h1 += h2;
            h2 += h1;

            h1 = Fmix64(h1);
            h2 = Fmix64(h2);

            return h1 + h2;
        }
    }
    
    private static ulong Fmix64(ulong hash)
    {
        unchecked
        {
            hash ^= hash >> 33;
            hash *= 0xff51afd7ed558ccdL;
            hash ^= hash >> 33;
            hash *= 0xc4ceb9fe1a85ec53L;
            hash ^= hash >> 33;

            return hash;
        }
    }

    public static ulong Hash64InternalUnrolled(ReadOnlySpan<byte> data, ulong seed)
    {
        unchecked
        {
            ulong h1 = seed;
            ulong h2 = seed;
            var length = data.Length;
            int nblocks = length >> 4;
            int nblocks4 = nblocks >> 2; // Number of 4-block chunks (64 bytes each)

            // Process 4 blocks (64 bytes) at a time
            for (int i = 0; i < nblocks4; i++)
            {
                int idx = (i << 6); // Multiply by 64 (4 * 16)

                // Block 0
                ulong kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx));
                ulong kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 8));

                kk1 *= C1;
                kk1 = BitOperations.RotateLeft(kk1, R1);
                kk1 *= C2;
                h1 ^= kk1;
                h1 = BitOperations.RotateLeft(h1, R2);
                h1 += h2;
                h1 = h1 * M + N1;

                kk2 *= C2;
                kk2 = BitOperations.RotateLeft(kk2, R3);
                kk2 *= C1;
                h2 ^= kk2;
                h2 = BitOperations.RotateLeft(h2, R1);
                h2 += h1;
                h2 = h2 * M + N2;

                // Block 1
                kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 16));
                kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 24));

                kk1 *= C1;
                kk1 = BitOperations.RotateLeft(kk1, R1);
                kk1 *= C2;
                h1 ^= kk1;
                h1 = BitOperations.RotateLeft(h1, R2);
                h1 += h2;
                h1 = h1 * M + N1;

                kk2 *= C2;
                kk2 = BitOperations.RotateLeft(kk2, R3);
                kk2 *= C1;
                h2 ^= kk2;
                h2 = BitOperations.RotateLeft(h2, R1);
                h2 += h1;
                h2 = h2 * M + N2;

                // Block 2
                kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 32));
                kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 40));

                kk1 *= C1;
                kk1 = BitOperations.RotateLeft(kk1, R1);
                kk1 *= C2;
                h1 ^= kk1;
                h1 = BitOperations.RotateLeft(h1, R2);
                h1 += h2;
                h1 = h1 * M + N1;

                kk2 *= C2;
                kk2 = BitOperations.RotateLeft(kk2, R3);
                kk2 *= C1;
                h2 ^= kk2;
                h2 = BitOperations.RotateLeft(h2, R1);
                h2 += h1;
                h2 = h2 * M + N2;

                // Block 3
                kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 48));
                kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 56));

                kk1 *= C1;
                kk1 = BitOperations.RotateLeft(kk1, R1);
                kk1 *= C2;
                h1 ^= kk1;
                h1 = BitOperations.RotateLeft(h1, R2);
                h1 += h2;
                h1 = h1 * M + N1;

                kk2 *= C2;
                kk2 = BitOperations.RotateLeft(kk2, R3);
                kk2 *= C1;
                h2 ^= kk2;
                h2 = BitOperations.RotateLeft(h2, R1);
                h2 += h1;
                h2 = h2 * M + N2;
            }

            // Process remaining blocks one at a time
            for (int i = nblocks4 << 2; i < nblocks; i++)
            {
                int idx = (i << 4);
                ulong kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx));
                ulong kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 8));

                kk1 *= C1;
                kk1 = BitOperations.RotateLeft(kk1, R1);
                kk1 *= C2;
                h1 ^= kk1;
                h1 = BitOperations.RotateLeft(h1, R2);
                h1 += h2;
                h1 = h1 * M + N1;

                kk2 *= C2;
                kk2 = BitOperations.RotateLeft(kk2, R3);
                kk2 *= C1;
                h2 ^= kk2;
                h2 = BitOperations.RotateLeft(h2, R1);
                h2 += h1;
                h2 = h2 * M + N2;
            }

            // Process remaining bytes (tail)
            ulong k1 = 0;
            ulong k2 = 0;
            int index = nblocks << 4;
            switch (length - index)
            {
                case 15:
                    k2 ^= ((ulong)data[index + 14] & 0xff) << 48;
                    goto case 14;
                case 14:
                    k2 ^= ((ulong)data[index + 13] & 0xff) << 40;
                    goto case 13;
                case 13:
                    k2 ^= ((ulong)data[index + 12] & 0xff) << 32;
                    goto case 12;
                case 12:
                    k2 ^= ((ulong)data[index + 11] & 0xff) << 24;
                    goto case 11;
                case 11:
                    k2 ^= ((ulong)data[index + 10] & 0xff) << 16;
                    goto case 10;
                case 10:
                    k2 ^= ((ulong)data[index + 9] & 0xff) << 8;
                    goto case 9;
                case 9:
                    k2 ^= (ulong)data[index + 8] & 0xff;
                    k2 *= C2;
                    k2 = BitOperations.RotateLeft(k2, R3);
                    k2 *= C1;
                    h2 ^= k2;
                    goto case 8;
                case 8:
                    k1 ^= ((ulong)data[index + 7] & 0xff) << 56;
                    goto case 7;
                case 7:
                    k1 ^= ((ulong)data[index + 6] & 0xff) << 48;
                    goto case 6;
                case 6:
                    k1 ^= ((ulong)data[index + 5] & 0xff) << 40;
                    goto case 5;
                case 5:
                    k1 ^= ((ulong)data[index + 4] & 0xff) << 32;
                    goto case 4;
                case 4:
                    k1 ^= ((ulong)data[index + 3] & 0xff) << 24;
                    goto case 3;
                case 3:
                    k1 ^= ((ulong)data[index + 2] & 0xff) << 16;
                    goto case 2;
                case 2:
                    k1 ^= ((ulong)data[index + 1] & 0xff) << 8;
                    goto case 1;
                case 1:
                    k1 ^= (ulong)data[index] & 0xff;
                    k1 *= C1;
                    k1 = BitOperations.RotateLeft(k1, R1);
                    k1 *= C2;
                    h1 ^= k1;
                    break;
            }

            // finalization
            h1 ^= (ulong)length;
            h2 ^= (ulong)length;

            h1 += h2;
            h2 += h1;

            h1 = Fmix64(h1);
            h2 = Fmix64(h2);

            return h1 + h2;
        }
    }

    // public static ulong Hash64Internal2(ReadOnlySpan<byte> data, ulong seed)
    // {
    //     unchecked
    //     {
    //         ulong h1 = seed;
    //         ulong h2 = seed;
    //         var length = data.Length;
    //
    //         // Process 32 bytes at a time using AVX2
    //         if (Avx2.IsSupported && length >= 32)
    //         {
    //             var c1Vec = Vector256.Create(C1);
    //             var c2Vec = Vector256.Create(C2);
    //             int nblocks = length / 32;
    //
    //             for (int i = 0; i < nblocks; i++)
    //             {
    //                 int idx = i * 32;
    //                 var block = MemoryMarshal.Cast<byte, Vector256<ulong>>(data.Slice(idx, 32))[0];
    //
    //                 // Process first pair (k1)
    //                 var k1k2 = block;
    //                 var temp1 = Avx2.Multiply(k1k2.GetLower(), c1Vec);
    //                 temp1 = Vector256.Create(
    //                     BitOperations.RotateLeft(temp1.GetElement(0), R1),
    //                     BitOperations.RotateLeft(temp1.GetElement(1), R1)
    //                 );
    //                 temp1 = Avx2.Multiply(temp1, c2Vec);
    //
    //                 h1 ^= temp1.GetElement(0);
    //                 h1 = BitOperations.RotateLeft(h1, R2);
    //                 h1 += h2;
    //                 h1 = h1 * M + N1;
    //
    //                 // Process second pair (k2)
    //                 var k3k4 = block.GetUpper();
    //                 var temp2 = Avx2.Multiply(k3k4, c2Vec);
    //                 temp2 = Vector256.Create(
    //                     BitOperations.RotateLeft(temp2.GetElement(0), R3),
    //                     BitOperations.RotateLeft(temp2.GetElement(1), R3)
    //                 );
    //                 temp2 = Avx2.Multiply(temp2, c1Vec);
    //
    //                 h2 ^= temp2.GetElement(0);
    //                 h2 = BitOperations.RotateLeft(h2, R1);
    //                 h2 += h1;
    //                 h2 = h2 * M + N2;
    //             }
    //
    //             // Update index for remaining bytes
    //             int processed = nblocks * 32;
    //             data = data.Slice(processed);
    //             length -= processed;
    //         }
    //
    //         // Process remaining blocks of 16 bytes
    //         int nblocks16 = length >> 4;
    //         for (int i = 0; i < nblocks16; i++)
    //         {
    //             int idx = (i << 4);
    //             ulong kk1 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx));
    //             ulong kk2 = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(idx + 8));
    //
    //             kk1 *= C1;
    //             kk1 = BitOperations.RotateLeft(kk1, R1);
    //             kk1 *= C2;
    //             h1 ^= kk1;
    //             h1 = BitOperations.RotateLeft(h1, R2);
    //             h1 += h2;
    //             h1 = h1 * M + N1;
    //
    //             kk2 *= C2;
    //             kk2 = BitOperations.RotateLeft(kk2, R3);
    //             kk2 *= C1;
    //             h2 ^= kk2;
    //             h2 = BitOperations.RotateLeft(h2, R1);
    //             h2 += h1;
    //             h2 = h2 * M + N2;
    //         }
    //
    //         // Process remaining bytes (tail)
    //         ulong k1 = 0;
    //         ulong k2 = 0;
    //         int index = nblocks16 << 4;
    //         switch (length - index)
    //         {
    //             case 15:
    //                 k2 ^= ((ulong)data[index + 14] & 0xff) << 48;
    //                 goto case 14;
    //             case 14:
    //                 k2 ^= ((ulong)data[index + 13] & 0xff) << 40;
    //                 goto case 13;
    //             case 13:
    //                 k2 ^= ((ulong)data[index + 12] & 0xff) << 32;
    //                 goto case 12;
    //             case 12:
    //                 k2 ^= ((ulong)data[index + 11] & 0xff) << 24;
    //                 goto case 11;
    //             case 11:
    //                 k2 ^= ((ulong)data[index + 10] & 0xff) << 16;
    //                 goto case 10;
    //             case 10:
    //                 k2 ^= ((ulong)data[index + 9] & 0xff) << 8;
    //                 goto case 9;
    //             case 9:
    //                 k2 ^= (ulong)data[index + 8] & 0xff;
    //                 k2 *= C2;
    //                 k2 = BitOperations.RotateLeft(k2, R3);
    //                 k2 *= C1;
    //                 h2 ^= k2;
    //                 goto case 8;
    //             case 8:
    //                 k1 ^= ((ulong)data[index + 7] & 0xff) << 56;
    //                 goto case 7;
    //             case 7:
    //                 k1 ^= ((ulong)data[index + 6] & 0xff) << 48;
    //                 goto case 6;
    //             case 6:
    //                 k1 ^= ((ulong)data[index + 5] & 0xff) << 40;
    //                 goto case 5;
    //             case 5:
    //                 k1 ^= ((ulong)data[index + 4] & 0xff) << 32;
    //                 goto case 4;
    //             case 4:
    //                 k1 ^= ((ulong)data[index + 3] & 0xff) << 24;
    //                 goto case 3;
    //             case 3:
    //                 k1 ^= ((ulong)data[index + 2] & 0xff) << 16;
    //                 goto case 2;
    //             case 2:
    //                 k1 ^= ((ulong)data[index + 1] & 0xff) << 8;
    //                 goto case 1;
    //             case 1:
    //                 k1 ^= (ulong)data[index] & 0xff;
    //                 k1 *= C1;
    //                 k1 = BitOperations.RotateLeft(k1, R1);
    //                 k1 *= C2;
    //                 h1 ^= k1;
    //                 break;
    //         }
    //
    //         // Finalization
    //         h1 ^= (ulong)length;
    //         h2 ^= (ulong)length;
    //
    //         h1 += h2;
    //         h2 += h1;
    //
    //         h1 = Fmix64(h1);
    //         h2 = Fmix64(h2);
    //
    //         return h1 + h2;
    //     }
    // }

}