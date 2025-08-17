using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace ApacheIgniteSimd;

public static class PartsForTests
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ulong> RotateLeft(Vector<ulong> v, int offset)
    {
        return (v << offset) | (v >> (64 - offset));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ulong> RotateLeft(Vector128<ulong> v, Vector128<ulong> offset)
    {
        return
            Sse2.Or(
            Sse2.ShiftLeftLogical(v, offset),
            Sse2.ShiftLeftLogical(v, (Vector128.Create((ulong)64, 64) - offset)));
        // return (v << offset) | (v >> (64 - offset)) ;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ulong> RotateLeftSse2(Vector128<ulong> v, byte offset)
    {
        return
            Sse2.Or(
            Sse2.ShiftLeftLogical(v, offset),
            Sse2.ShiftRightLogical(v, (byte)(64 - offset)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<ulong> RotateLeftOperators(Vector128<ulong> v, byte offset)
    {
        return (v << offset) | (v >> (64 - offset)) ;
    }
}