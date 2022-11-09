using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<DictionaryKeys>();

[MemoryDiagnoser]
public class DictionaryKeys
{
    private Dictionary<int, int> Ints = new()
    {
        { 1, 1 },
        { 2, 1 }
    };

    private Dictionary<Class, int> Classes = new()
    {
        { new() { I = 1 }, 1 },
        { new() { I = 2 }, 1 }
    };
    
    private Dictionary<Struct, int> Structs = new()
    {
        { new() { I = 1 }, 1 },
        { new() { I = 2 }, 1 }
    };
    
    private Dictionary<EquatableStruct, int> EStructs = new()
    {
        { new() { I = 1 }, 1 },
        { new() { I = 2 }, 1 }
    };


    private Class C = new() { I = 1 };

    public DictionaryKeys()
    {
        
    }

    [Benchmark]
    public bool ContainsInt() => Ints.ContainsKey(1);

    [Benchmark]
    public bool ContainsClass() => Classes.ContainsKey(C);
    
    [Benchmark]
    public bool ContainsStruct() => Structs.ContainsKey(new(){I = 1});
    
    [Benchmark]
    public bool ContainsEStruct() => EStructs.ContainsKey(new(){I = 1});
    
    public class Class
    {
        public int I { get; set; }

        protected bool Equals(Class other) => I == other.I;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class)obj);
        }

        public override int GetHashCode() => I;
    }
    
    public struct Struct
    {
        public int I { get; set; }
        // public override bool Equals(object? obj)
        // {
        //     Console.WriteLine("Struct Equals");
        //     return base.Equals(obj);
        // }
    }

    public struct EquatableStruct : IEquatable<EquatableStruct>
    {
        public int I { get; set; }

        public bool Equals(EquatableStruct other) => I == other.I;

        public override bool Equals(object? obj)
        {
            if (obj is EquatableStruct other && Equals(other)) return true;
            return false;
        }

        public override int GetHashCode() => I;
    }
}
