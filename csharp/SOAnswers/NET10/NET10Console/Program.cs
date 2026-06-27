// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json;

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using HighPerfPlayground;

using (var mo = Tada.Do())
{
    System.Console.WriteLine(mo.Length);
    System.Console.WriteLine(mo.Span[0]);
    System.Console.WriteLine(mo.Span[mo.Length-1]);
}
Environment.Exit(0);
var foo = JsonSerializer.Deserialize<Foo>(
        """
        {
            "Hello": "World",
            "Bar": { "$type": "unknown", "Test": 1 }
        }
        """);

Environment.Exit(0);

NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=6432;Database=test_db;Username=postgres;Password=P@ssword");
connection.Open();
NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from public.t4(@iid)", connection);
cmd.Parameters.Add(new NpgsqlParameter("iid", DbType.Int32) { Direction = ParameterDirection.Input });
cmd.Parameters[0].Value = 1;
cmd.Parameters.Add(new NpgsqlParameter("n1", DbType.String) { Direction = ParameterDirection.Output });
cmd.Parameters.Add(new NpgsqlParameter("n2", DbType.String) { Direction = ParameterDirection.Output });
cmd.ExecuteNonQuery();
var t1 = cmd.Parameters[1].Value.ToString();
var t2 = cmd.Parameters[2].Value.ToString();

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


// [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true,
//     UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
// [JsonDerivedType(typeof(KnownBar), nameof(KnownBar))]
[JsonConverter(typeof(BarConverter))]
interface IBar { }

class KnownBar : IBar { }
class AnotherKnownBar : IBar 
{
    public int Test { get; set; }
}

class Foo
{
    public required string Hello { get; init; }
    public required Bar? Bar { get; init; }
}


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

class BarConverter : JsonConverter<IBar>
{
    public override IBar? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Copy reader so we can advance the token stream separately.
        var copy = reader;
        copy.Read();
        var discriminatorProperty = copy.GetString();
        copy.Read();
        var discriminatorValue = copy.GetString();
        var targetType = discriminatorValue switch
        {
            "KnownBar" => typeof(KnownBar),
            "AnotherKnownBar" => typeof(AnotherKnownBar),
            _ => typeof(KnownBar) // Fallback
        };
        

        return JsonSerializer.Deserialize(ref reader, targetType, options) as IBar;
    }

    public override void Write(Utf8JsonWriter writer, IBar value, JsonSerializerOptions options) => throw new NotImplementedException();
}

[JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
[JsonDerivedType(typeof(KnownBarViaConcrete), nameof(KnownBarViaConcrete))]
public class Bar { } // will fallback to it

public class KnownBarViaConcrete : Bar { }
public class Poco
{
    public long Key { get; set; }

    public string? Val { get; set; }

    [NotMapped]
    public Guid UnmappedId { get; set; }

    [NotMapped]
    public string? UnmappedStr { get; set; }

    public override string ToString() => $"Poco [Key={Key}, Val={Val}]";
}


public interface IOperation
{
    string Execute();
}

public class OperationA : IOperation
{
    public string Execute() => "OperationA executed";
}

public class OperationB : IOperation
{
    public string Execute() => "OperationB executed";
}

public class Processor
{
    private readonly IServiceProvider _serviceProvider;

    // Is injecting IServiceProvider here a bad practice (Service Locator)?
    public Processor(IServiceProvider serviceProvider) 
    { 
        _serviceProvider = serviceProvider; 
    }  

    public void Process(string input)
    {
        // Resolving dynamically based on a key
        var op = _serviceProvider.GetKeyedService<IOperation>(input.ToLower());

        if (op == null)
        {
            Console.WriteLine("Invalid input: Operation not found.");
            return;
        }

        Console.WriteLine(op.Execute());
    }
}

public static class KeyedHelpers
{
    extension (IServiceCollection services)
    {
        public IServiceCollection RegisterKeyedResolver()
        {
            services.AddSingleton(new ServiceCollectionWrapper(services));
            services.AddSingleton(typeof(KeyedServicesKeysCacher<,>));

            return services;
        }
    }

    private record ServiceCollectionWrapper(IServiceCollection Services);

    private class KeyedServicesKeysCacher<TKey, TService>
    {
        public KeyedServicesKeysCacher(ServiceCollectionWrapper servicesProvider)
        {
            Keys = servicesProvider
                .Services
                .Where(sd => sd.ServiceType == typeof(TService))
                .Where(sd => sd.ServiceKey is not null)
                .Select(sd => sd.ServiceKey)
                .OfType<TKey>()
                .ToArray();
        }

        public TKey[] Keys {get;}
    }
}

public interface IKeyedResolver<TKey, TService>
{
    
}
