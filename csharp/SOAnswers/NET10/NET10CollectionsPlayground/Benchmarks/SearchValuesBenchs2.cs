using System.Buffers;
using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
public class SearchValuesBenchs2
{
    [ParamsSource(nameof(StringValues))]
    public string String { get; set; }
    private static HashSet<char> CharsHashset = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9' , '.'];
    private static FrozenSet<char> CharsFrozenset = CharsHashset.ToFrozenSet();
    private static SearchValues<char> CharsSearchValues = SearchValues.Create(CharsHashset.ToArray());
    
    [Benchmark]
    public bool ViaHashSet()
    {
        for (var index = 0; index < String.Length; index++)
        {
            var c = String[index];
            if (!CharsHashset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaFrozenSet()
    {
        for (var index = 0; index < String.Length; index++)
        {
            var c = String[index];
            if (!CharsFrozenset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValues()
    {
        for (var index = 0; index < String.Length; index++)
        {
            var c = String[index];
            if (!CharsSearchValues.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValuesIndexOfAny() => String.AsSpan().IndexOfAnyExcept(CharsSearchValues) >= 0;
    
    public static IEnumerable<string> StringValues()
    {
        yield return "54323.5";
        yield return "543g23.5";
    }
}