using System.Linq.Expressions;

namespace WhatsNewCSharp14.Features;

public class FirstClassSpans
{
    public static void Do()
    {
        // Rider provides warning:
        // The resolution for this invocation has changed in C# 14
        // due to a breaking change in overload resolution with spans.
        // This can cause runtime exceptions when compiled with interpretation.
        Expression<Func<int[], bool>> predicate = x => x.Contains(0);
        var methodInfo = (predicate.Body as MethodCallExpression)!.Method;

        Console.WriteLine(methodInfo.DeclaringType); // System.MemoryExtensions
        Console.WriteLine(methodInfo); // Boolean Contains[Int32](System.ReadOnlySpan`1[System.Int32], Int32)
        
        // Define custom ones
        new long[1].DoItWithLong();

        // Covariance
        ICovariant<MBase> _ = new MyCovariant<MDerived>();
        Span<ICovariant<MDerived>>.Empty.DoItVariance();
        
        Action<int> a = new int[0].M; // binds to M<int>(IEnumerable<int>, int)
        a(1); // Prints "IEnumerable M in Action"
    }
}


file static class E
{
    public static void M<T>(this Span<T> s, T x) => Console.Write("Span M in Action");
    public static void M<T>(this IEnumerable<T> e, T x) => Console.Write("IEnumerable M in Action");
}

file static class SpanExtensionMethods
{
    public static void DoItWithLong(this ReadOnlySpan<long> _){}
}


file static class SpanExtensionCovarianceMethods
{
    public static void DoItVariance(this ReadOnlySpan<ICovariant<MBase>> _){}
}

file interface ICovariant<out TR> { TR GetSomething(); }

file class MBase; 
file class MDerived : MBase;

file class MyCovariant<TR> : ICovariant<TR>
{
    public TR GetSomething() => default!;
}