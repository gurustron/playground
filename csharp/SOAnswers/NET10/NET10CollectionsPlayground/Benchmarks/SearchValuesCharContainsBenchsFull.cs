using System.Buffers;
using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;

namespace NET10CollectionsPlayground.Benchmarks;

[MemoryDiagnoser(true)]
[DisassemblyDiagnoser]
public class SearchValuesCharContainsBenchsFull
{
    [ParamsSource(nameof(TestedStringData))]
    public string TestedString { get; set; } = null!;
    
    [ParamsSource(nameof(SearchValuesData))]
    public string Values = null!;

    public HashSet<char> CharsHashset = null!;

    public FrozenSet<char> CharsFrozenset = null!;
    
    public SearchValues<char> CharsSearchValues  = null!;  

    [GlobalSetup]
    public void GlobalSetup()
    {
        CharsHashset = [.. Values];
        CharsFrozenset = Values.ToFrozenSet();
        CharsSearchValues = SearchValues.Create(Values);
    }

    [Benchmark]
    public bool ViaHashSet()
    {
        for (var index = 0; index < TestedString.Length; index++)
        {
            var c = TestedString[index];
            if (CharsHashset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaFrozenSet()
    {
        for (var index = 0; index < TestedString.Length; index++)
        {
            var c = TestedString[index];
            if (CharsFrozenset.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValues()
    {
        for (var index = 0; index < TestedString.Length; index++)
        {
            var c = TestedString[index];
            if (CharsSearchValues.Contains(c)) return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool ViaCharsSearchValuesIndexOfAny() => TestedString.IndexOfAny(CharsSearchValues) >= 0;
    
    public static IEnumerable<string> TestedStringData()
    {
        yield return "a";
        yield return "1";
        yield return "ab";
        yield return "ba";
        yield return "11";
        yield return "a11";
        yield return "11a";
        yield return "111";
        yield return "MEDSN_1";
        yield return "MEDSC_a";
        yield return "LONGSTRINGNO_11111_11111_11111_11111_11111";
        yield return "LONGSTRINGCONTaINS_11111_11111_11111_11111_11111";
        yield return "LONGSTRING_11111_11111_11111_11111_11111_CONTaINS";
    }

    public static IEnumerable<string> SearchValuesData()
    {
        yield return "a";
        yield return "az";   
        yield return "abcdefghz02345679";   
    }
}