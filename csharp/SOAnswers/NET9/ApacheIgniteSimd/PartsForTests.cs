using System.Numerics;
using System.Runtime.CompilerServices;

namespace ApacheIgniteSimd;

public static class PartsForTests
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ulong> RotateLeft(Vector<ulong> v, int offset)
    {
        return (v << offset) | (v >> (64 - offset)) ;
    }
}