using System.Runtime.CompilerServices;

namespace NET10.Benchs;

public static class OuterClass
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static int Method1() => Enumerable.Range(0, 100).Sum();
}