// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using UnitsNet;
using UnitsNet.Units;

async Task TestAsyncEnumerable(string name, IAsyncEnumerable<Task<int>> tasks)
{
    Console.WriteLine($"Before: {name}");
    await foreach (var t in tasks)
    {
        var i = await t;
        Console.WriteLine($"\t\t{name}: {i}");
    }
    Console.WriteLine($"Between: {name}");
    await foreach (var t in tasks)
    {
        var i = await t;
        Console.WriteLine($"\t\t{name}: {i}");
    }
    Console.WriteLine($"After: {name}");
}

async IAsyncEnumerable<Task<int>> GenerateAsyncEnumerable()
{
    await Task.Yield();
    for (int j = 0; j < 3; j++)
    {
        yield return Task.FromResult(j);
    }
}

await TestAsyncEnumerable(nameof(GenerateAsyncEnumerable), GenerateAsyncEnumerable());

Task<int>[] tasks = [Task.FromResult(0), Task.FromResult(1), Task.FromResult(2)];

await TestAsyncEnumerable(nameof(Task.WhenEach), Task.WhenEach(tasks));

Environment.Exit(0);




var myObject = new MyObject(new MyKey(Guid.Empty), "Test", new AmountPayed("MyC", 2));
var serialize = JsonSerializer.Serialize(myObject);

string password = "password123!";
string b64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(password));
string b641 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
string b6412 = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(password));
ServiceCollection services = new ServiceCollection();

services.AddScoped<IValidator<object, Cat>, MyClass<object, Cat>>();
services.AddScoped<IValidator<object, Dog>, MyClass<object, Dog>>();
services.AddScoped<IValidator<Cat, Dog>, MyClass<Cat, Dog>>();
services.AddSingleton<IGenericInterface<Dog>, ConcreteClassA>();
services.AddSingleton<IInterface>(sp => sp.GetRequiredService<IGenericInterface<Dog>>());
services.AddSingleton<IInterface, ConcreteClassB>();
services.AddSingleton<ConcreteClassFactory>();

ServiceProvider sp = services.BuildServiceProvider();
using (IServiceScope scope = sp.CreateScope())
{
    IEnumerable<IValidator<object, Cat>>? validators =
        scope.ServiceProvider.GetService<IEnumerable<IValidator<object, Cat>>>();
}

ConcreteClassFactory t = sp.GetRequiredService<ConcreteClassFactory>();

Console.WriteLine(t.Get<Cat>());
Environment.Exit(0);
t.Do(new Cat());
AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
AssemblyBuilder ab =
    AssemblyBuilder.DefineDynamicAssembly(
        aName,
        AssemblyBuilderAccess.Run);

// The module name is usually the same as the assembly name.
ModuleBuilder mb = ab.DefineDynamicModule(aName.Name ?? "DynamicAssemblyExample");

TypeBuilder tb = mb.DefineType(
    "MyDynamicType",
    TypeAttributes.Public);

FieldInfo firstOrDefault = typeof(TestFuncPointers).GetFields().FirstOrDefault();
Type type = firstOrDefault.FieldType;
Type type1 = typeof(delegate*<int, int>);
bool b = type1 == type;
Type makePointerType = typeof(Func<int>).MakePointerType();


Console.WriteLine("Hello, World!");
int i = 0;
long totalAllocatedBytes = GC.GetTotalAllocatedBytes(true);
Console.WriteLine(new object());
for (int j = 0; j < 10_000; j++)
{
    i = 100L.ToNumberOrDefault(1);
}

for (int j = 0; j < 10_000; j++)
{
    i = 100.ToNumberOrDefault(1);
}


Console.WriteLine(GC.GetTotalAllocatedBytes(true) - totalAllocatedBytes);

public static class Exts
{
    public static TDestination ToNumberOrDefault<TSource, TDestination>(this TSource number, TDestination defaultValue)
        where TSource : struct, INumber<TSource>
        where TDestination : struct, INumber<TDestination>
    {
        try
        {
            return TDestination.CreateChecked(number);
        }
        catch
        {
            return defaultValue;
        }
    }
}

public class C
{
    public bool M<TOther>(TOther value)
    {
        if (typeof(TOther) == typeof(long))
        {
            long actualValue = (long)(object)value;
            Console.WriteLine(actualValue);
            return true;
        }

        return false;
    }
}

public unsafe class TestFuncPointers
{
    public delegate*<int, int> I;
}

public interface IInterface
{
    Type AnimalType { get; }
    void Do(Animal animal);
}

public interface IGenericInterface<T> : IInterface
{
    void Do(T obj);
}

public abstract class Animal
{
}

public class Dog : Animal
{
}

public class Cat : Animal
{
}

public abstract class ConcreteClassBase<T> : IGenericInterface<T>, IInterface where T : Animal
{
    public abstract void Do(T obj);

    void IInterface.Do(Animal animal) => Do((T)animal);
    public Type AnimalType => typeof(T);
}

public class ConcreteClassA : ConcreteClassBase<Dog>
{
    public override void Do(Dog obj)
    {
    }
}

public class ConcreteClassB : ConcreteClassBase<Cat>
{
    public override void Do(Cat obj)
    {
        Console.WriteLine("cat");
    }
}

public class ConcreteClassFactory
{
    private readonly Dictionary<Type, IInterface> _dictionary;

    public ConcreteClassFactory(IEnumerable<IInterface> concretes)
    {
        _dictionary = concretes.ToDictionary(i => i.AnimalType, i => i);
    }

    public void Do(Animal animal) => _dictionary[animal.GetType()].Do(animal);
    public IGenericInterface<T> Get<T>() => (IGenericInterface<T>)_dictionary[typeof(T)];
}

public interface IValidator<in TModel, in TContext>;

class MyClass<TModel, TContext> : IValidator<TModel, TContext>
{
    public MyClass()
    {
        Console.WriteLine($"MyClass: {typeof(TModel)} - {typeof(TContext)}");
    }
}


// [JsonConverter(typeof(MyKeyConverter))]
record MyKey(Guid Value);

// [JsonConverter(typeof(AmountPayedConverter))]
record AmountPayed(string Currency, decimal Value);

[JsonConverter(typeof(MyObjectConverter))]
record MyObject(MyKey Id, string Name, AmountPayed AmountPayed);

class MyObjectConverter : JsonConverter<MyObject>
{
    public override MyObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, MyObject value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Id", value.Id.Value);
        writer.WriteString("Name", value.Name);
        writer.WriteString("Currency", value.AmountPayed.Currency);
        writer.WriteNumber("Amount", value.AmountPayed.Value);
        writer.WriteEndObject();
    }
}

class MyKeyConverter : JsonConverter<MyKey>
{
    public override MyKey Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, MyKey value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}

class AmountPayedConverter : JsonConverter<AmountPayed>
{
    public override AmountPayed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AmountPayed value, JsonSerializerOptions options)
    {
        writer.WriteString("Currency", value.Currency);
        writer.WriteNumber("Amount", value.Value);
    }
}