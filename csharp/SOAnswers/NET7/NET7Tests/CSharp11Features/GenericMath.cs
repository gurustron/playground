using System.Numerics;

namespace NET7Tests.CSharp11Features;

public class GenericMath
{
    [Test]
    public void TestCustomExtensionMethods()
    {
        var average = new List<int>
            {
                1,2,3
            }
            .Average<int, int>();
        
        Assert.That(average, Is.EqualTo(2));
    }
}

file static class Exts
{
    public static TResult Sum<TInput, TResult>(this IEnumerable<TInput> numbers) where TInput : INumber<TInput>
        where TResult : INumber<TResult> => numbers.Aggregate(TResult.Zero, (agg, t) => agg + TResult.CreateChecked(t));
    
    public static TResult Average<TInput, TResult>(this IEnumerable<TInput> numbers)
        where TInput : INumber<TInput>
        where TResult : INumber<TResult>
    {
        if (!numbers.TryGetNonEnumeratedCount(out var count))
        {
            var enumerable = numbers as ICollection<TInput> ?? numbers.ToArray();
            return TResult.CreateChecked(enumerable.Sum<TInput, TResult>()) / TResult.CreateChecked(enumerable.Count);
        }

        return TResult.CreateChecked(numbers.Sum<TInput, TResult>()) / TResult.CreateChecked(count);
    }
}