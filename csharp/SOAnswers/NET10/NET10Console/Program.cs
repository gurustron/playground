// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.ComponentModel;
using System.Text.Json;

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();


services.AddHybridCache();

var sp = services.BuildServiceProvider();
var cache = sp.GetRequiredService<HybridCache>();

var ssv = SearchValues.Create(["one", "two"], StringComparison.OrdinalIgnoreCase);

var r = new[] { "test", "this contains one", "one" }.AsSpan().IndexOfAny(ssv);
var ofAny = "this contains one".AsSpan().IndexOfAny(ssv);
var ofAny1 = "this contains one".IndexOfAny(ssv);

Match m = Regex.Match("abc", "(?=(abc))");
Debug.Assert(m.Success);

foreach (Group g in m.Groups)
{
    foreach (Capture c in g.Captures)
    {
        Console.WriteLine($"Group: {g.Name}, Capture: {c.Value}");
    }
}

Console.WriteLine();

public static class Exts
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public IEnumerable<TSource> DistinctLastBy<TKey>(Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer = default)
        {
            return source
                .Reverse()
                .DistinctByStable(keySelector, comparer)
                .Reverse();
        }

        private IEnumerable<TSource> DistinctByStable<TKey>(Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            using IEnumerator<TSource> enumerator = source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var set = new HashSet<TKey>(comparer); // TODO - play with size?
                do
                {
                    TSource element = enumerator.Current;
                    if (set.Add(keySelector(element)))
                    {
                        yield return element;
                    }
                }
                while (enumerator.MoveNext());
            }
        }
    }
}
