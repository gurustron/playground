using System.Buffers;
using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
public class SearchValuesStringCollectionBenchs
{
    [ParamsSource(nameof(TestedStringsData))]
    public string[] TestedStrings { get; set; } = null!;

    private static HashSet<string> StringsHashset = new(StringComparer.InvariantCultureIgnoreCase)
    {
        "somelongstring",
        "foo",
        "bar",
        "42",
        "somelongstring_1234567890"
    };
    private static FrozenSet<string> StringsFrozenset = StringsHashset.ToFrozenSet(StringsHashset.Comparer);
    private static SearchValues<string> StringsSearchValues = SearchValues.Create(StringsHashset.ToArray(), StringComparison.InvariantCulture);
    
    [Benchmark]
    public bool ViaHashSet()
    {
        for (var index = 0; index < TestedStrings.Length; index++)
        {
            var c = TestedStrings[index];
            if (!StringsHashset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaFrozenSet()
    {
        for (var index = 0; index < TestedStrings.Length; index++)
        {
            var c = TestedStrings[index];
            if (!StringsFrozenset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValues()
    {
        for (var index = 0; index < TestedStrings.Length; index++)
        {
            var c = TestedStrings[index];
            if (!StringsSearchValues.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValuesIndexOfAny() => TestedStrings.AsSpan().IndexOfAnyExcept(StringsSearchValues) >= 0;
    
    public static IEnumerable<string[]> TestedStringsData()
    {
        yield return [];
        yield return ["1"];
        yield return ["somelongstring_1234567890"];
        yield return [$"some_very_long_string_{string.Join("_", Enumerable.Range(0, 1000))}"];
        yield return Enumerable.Range(777, 10).Select(i => i.ToString()).ToArray();
        yield return Enumerable.Range(777, 10).Select(i => i.ToString())
            .Append("somelongstring_1234567890")
            .ToArray();
        yield return Enumerable.Range(777, 1000).Select(i => i.ToString()).ToArray();
        yield return new[]{"somelongstring_1234567890"}
            .Union(Enumerable.Range(777, 1000).Select(i => i.ToString()))
            .ToArray();
    }
}