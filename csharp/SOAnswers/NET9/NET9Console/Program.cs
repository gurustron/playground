using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Supabase.Interfaces;
using Supabase.Postgrest;
using Supabase.Postgrest.Models;
using UnitsNet;
using UnitsNet.Units;

var tuple1 = (x:2,y:4);
var tuple2 = (x:0,y:-1);
var tuple3 = tuple1.Add(tuple2);
int[,] array =
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

Memory2D<int> memory = array;
var memoryLength = memory.Length.ToInt32();
var dest = new int[memory.Length];
memory.CopyTo(dest);
Task.Run(async () =>
{
    await Task.Delay(100);
    return (1, 100);
});

int[] delays = [100, 50, 0];

var tasksToRun = delays
    .Select(async (delay, index) =>
    {
        await Task.Delay(delay);
        return (index, delay);
    });
var sw = Stopwatch.StartNew();
await foreach (var completed in Task.WhenEach(tasksToRun))
{
    Console.Write($"{completed.Status} ");
    var result = completed.Result;
    Console.WriteLine($"index: {result.index} delay: {result.delay} elapsed: {sw.ElapsedMilliseconds}ms");
    break;
}

Environment.Exit(0);

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


public static class TupleExts
{
    public static (TX X, TY Y) Add<TX, TY>(this (TX X, TY Y) left, (TX X, TY Y) right)
        where TX : IAdditionOperators<TX, TX, TX>
        where TY : IAdditionOperators<TY, TY, TY> =>
        (left.X + right.X, left.Y + right.Y);
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public static Point operator +(Point left, Point right) => new Point
    {
        X = left.X + right.X,
        Y = left.Y + right.Y
    };
}
