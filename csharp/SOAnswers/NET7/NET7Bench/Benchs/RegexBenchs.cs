using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace NET7Bench.Benchs;

[MemoryDiagnoser]
public partial class RegexBenchs
{
    private static string[] _strings = new[]
    {
        "asdasdasd",
        "1233431432108-235-0120",
        "JJKWQEHM<A<AMSnmsdqjhjMWNQMNW",
        "def",
        "cxcvnnmeoufyre75"
    };
    private static readonly Regex s_abcOrDefGeneratedRegex =
        new(pattern: "abc|def",
            options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    private static readonly Regex s_abcOrDefGeneratedRegexNonCompiled =
        new(pattern: "abc|def",
            options: RegexOptions.IgnoreCase);
    
    [GeneratedRegex("abc|def", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-US")]
    private static partial Regex AbcOrDefGeneratedRegex();
    
    
    [Benchmark]
    public bool CompiledStatic()
    {
        bool result = false;
        foreach (var s in _strings)
        {
            result |= s_abcOrDefGeneratedRegex.IsMatch(s);
        }

        return result;
    }
        
    [Benchmark]
    public bool NonCompiledStatic()
    {
        bool result = false;
        foreach (var s in _strings)
        {
            result |= s_abcOrDefGeneratedRegexNonCompiled.IsMatch(s);
        }

        return result;
    }
    
    [Benchmark]
    public bool Generated()
    {
        bool result = false;
        foreach (var s in _strings)
        {
            result |= AbcOrDefGeneratedRegex().IsMatch(s);
        }

        return result;
    }
}