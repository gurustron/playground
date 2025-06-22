namespace NET9.Benchs;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Frozen;

[MemoryDiagnoser]
public class DictionaryVsFrozenDictionaryBenchmarks
{
    private KeyValuePair<string, string>[] _source;
    private Dictionary<string, string> _dictionary;
    private FrozenDictionary<string, string> _frozen;
    private string _middleKey;
    private string _middleKeyPlusOne;

    [Params(10, 100, 10_000)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        _source = Enumerable.Range(0, Size)
            .Select(i => new KeyValuePair<string, string>($"Key{i}", $"Value{i}"))
            .ToArray();
        _middleKey = $"Key{Size / 2}";
        _middleKeyPlusOne = $"Key{Size / 2 + 1}";

        _dictionary = new Dictionary<string, string>(_source);
        _frozen = _dictionary.ToFrozenDictionary();
    }

    // [Benchmark]
    // public Dictionary<string, string> Dictionary_Create() => new Dictionary<string, string>(_source);
    //
    // [Benchmark]
    // public FrozenDictionary<string, string> FrozenDictionary_Create()
    // {
    //     return new Dictionary<string, string>(_source).ToFrozenDictionary();
    // }

    // [Benchmark]
    // public string Dictionary_Read1() => _dictionary[_middleKey];
    //
    // [Benchmark]
    // public string FrozenDictionary_Read1() => _frozen[_middleKey];
    //
    // [Benchmark]
    // public string Dictionary_Read2() => _dictionary[_middleKeyPlusOne];
    //
    // [Benchmark]
    // public string FrozenDictionary_Read2() => _frozen[_middleKeyPlusOne];
    
    [Benchmark]
    public int Dictionary_Iteration()
    {
        int count = 0;

        foreach (var kvp in _dictionary)
        {
            count += kvp.Value.Length;
        }

        return count;
    }

    [Benchmark]
    public int FrozenDictionary_Iteration()
    {
        int count = 0;

        foreach (var kvp in _frozen)
        {
            count += kvp.Value.Length;
        }

        return count;
    }
    
    [Benchmark]
    public int Dictionary_Keys_Iteration()
    {
        int count = 0;

        foreach (var kvp in _dictionary.Keys)
        {
            count += kvp.Length;
        }

        return count;
    }

    [Benchmark]
    public int FrozenDictionary_Keys_Iteration()
    {
        int count = 0;

        foreach (var kvp in _frozen.Keys)
        {
            count += kvp.Length;
        }

        return count;
    }

    [Benchmark]
    public int Dictionary_Values_Iteration()
    {
        int count = 0;

        foreach (var kvp in _dictionary.Values)
        {
            count += kvp.Length;
        }

        return count;
    }

    [Benchmark]
    public int FrozenDictionary_Values_Iteration()
    {
        int count = 0;

        foreach (var kvp in _frozen.Values)
        {
            count += kvp.Length;
        }

        return count;
    }
}


