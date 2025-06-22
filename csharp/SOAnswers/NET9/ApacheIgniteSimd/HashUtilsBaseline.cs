using System.Buffers.Binary;
using System.Numerics;

namespace ApacheIgniteSimd;

public class HashUtilsBaseline
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
        var hash64 = Hash64Internal(data, seed);

        return (int)(hash64 ^ (hash64 >> 32));
    }

    public static ulong Hash64Internal(ReadOnlySpan<byte> data, ulong seed)
    {
        unchecked
        {
            ulong h1 = seed;
            ulong h2 = seed;
            var length = data.Length;
            int nblocks = length >> 4;

            // body
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
}