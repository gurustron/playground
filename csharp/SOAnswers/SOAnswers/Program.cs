// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using OneOf;
using Moq;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;


var dataTable1 = new DataTable();
dataTable1.Columns.Add("Id");
dataTable1.Columns.Add("Name");
dataTable1.Columns.Add("Surname");
dataTable1.Rows.Add("1", "Mike", "Tyson");
dataTable1.Rows.Add("2", "John", "Wick");
dataTable1.PrimaryKey = new [] { dataTable1.Columns["Id"] };
var dataTable2 = new DataTable();
dataTable2.Columns.Add("Id");
dataTable2.Columns.Add("Country");
dataTable2.Columns.Add("Age");

dataTable2.PrimaryKey = new [] { dataTable2.Columns["Id"] };
dataTable2.Rows.Add("1", "America", "35");
dataTable2.Rows.Add("2", "Brasil", "50");

dataTable1.Merge(dataTable2);

int couter = 0;
var concurrentBag = new ConcurrentBag<int>();
// int maxSimultaneousThreads = 10;
// var throttler = new SemaphoreSlim(maxSimultaneousThreads, maxSimultaneousThreads); // maxSimultaneousThreads = 10
var unprocessedDictionary = new ConcurrentDictionary<int, int>();
for (int i = 0; i < 20; i++)
{
    unprocessedDictionary.AddOrUpdate(i, i, (i1, i2) => i);
}

var tasks = unprocessedDictionary.Select(async key =>
{
    try
    {
        if (unprocessedDictionary.TryRemove(key.Key, out var item))
        {
            var x = Interlocked.Increment(ref couter);
            concurrentBag.Add(x);
            await Task.Delay(100);
        }
    }
    catch (Exception ex)
    {
        // handle error
    }
    finally
    {
        
        Interlocked.Decrement(ref couter);
    }
});
await Task.WhenAll(tasks);
var max = concurrentBag.Max();


IServiceCollection sc = new ServiceCollection();
sc.AddScoped<IScoped, Scoped>();
sc.AddTransient<ITransient, Transient>();

var serviceProvider = sc.BuildServiceProvider(validateScopes: true);

// var transient = serviceProvider.GetService<ITransient>();


byte[] Do()
{
    var bytes = new byte[8];
    var cast = MemoryMarshal.Cast<byte, uint>(bytes.AsSpan());
    uint value = 0xFF_CC_BB_AA;
    cast.Fill(value);
    return bytes;
}

IEnumerator<int> e = CountTo(5);
string json = JsonSerializer.Serialize(e, new JsonSerializerOptions
{
    IncludeFields = true
});
Console.WriteLine(json);
var type = e.GetType();
var methodInfo = typeof(JsonSerializer)
    .GetMethod(nameof(JsonSerializer.Deserialize), BindingFlags.Static | BindingFlags.Public, new[]
    {
        typeof(string), typeof(JsonSerializerOptions)
    })
    .MakeGenericMethod(type);
var invoke = methodInfo.Invoke(null, new object[] { json , null});
var f = JsonSerializer.Deserialize<IEnumerable<int>> (json); // crash because interface

var enumerator = f.GetEnumerator();
while (enumerator.MoveNext());
static IEnumerator<int> CountTo(int end)
{
    for(int i = 1; i <= end; i++) 
    {
        Console.WriteLine("i = " + i);
        yield return i;
    }
}

var data = "Sat Sep 10 05:57:59 KST 2022";
var time = DateTime.ParseExact(data, "ddd MMM d hh:mm:ss \\K\\S\\T yyyy", null);

Console.WriteLine(time);
Console.WriteLine(UInt32.MaxValue);
var sw = Stopwatch.StartNew();
int capacity = 10;
var bounded = Channel.CreateBounded<int>(capacity);

var t1 = Task.Run(async () =>
{
    for (int i = 0; i < capacity; i++)
    {
        await Task.Delay(500);
        await bounded.Writer.WriteAsync(i);
    }
    bounded.Writer.Complete();
});

var t2 = Task.Run(async () =>
{
    while (await bounded.Reader.WaitToReadAsync())
    {
        var readAsync = await bounded.Reader.ReadAsync();
        await Task.Delay(200);
    }
});

await Task.WhenAll(t1, t2);
Console.WriteLine(sw.ElapsedMilliseconds);
    

Console.WriteLine(Convert.ToUInt32("111",2));
Console.WriteLine();


public interface IContext
{
    object? MainObject { get; }
}

public interface ITransient
{
}

class Transient : ITransient
{
    private readonly IScoped _scoped;

    public Transient(IScoped scoped)
    {
        _scoped = scoped;
    }
}

interface IScoped
{
}

class Scoped : IScoped
{
}


public interface IContext<T> : IContext
{
    new T? MainObject { get; set; }
}

public class Context<T> : IContext<T>
{
    public T? MainObject { get; set; }
    object? IContext.MainObject => MainObject;

    public Context(T? obj)
    {
        MainObject = obj;
    }
}

public class BaseValidator
{
    public BaseValidator(Context context) {}
}

public class Context:IDisposable
{
    public void Dispose()
    {
    }
}
public interface IContextProvider 
{
    Context Create();
}

public class EntityValidator: BaseValidator , IDisposable
{
    private readonly Context _context;
    public EntityValidator(IContextProvider provider) : this(new CaptureContextProvider(provider)) 
    {
    }
    
    private EntityValidator(CaptureContextProvider provider) : base(provider.Create()) 
    {
        _context = provider.Create();
    }
    class CaptureContextProvider : IContextProvider
    {
        private readonly IContextProvider _contextProvider;
        private Context? _capture;

        public CaptureContextProvider(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Context Create()
        {
            _capture ??= _contextProvider.Create();
            return _capture;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

public class Part : IEquatable<Part>
{
    public string PartName { get; set; }
    public int PartId { get; set; }

    
    //other code

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Part objAsPart = obj as Part;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    

    // other code

    public bool Equals(Part other)
    {
        if (other == null) return false;
        return (this.PartId.Equals(other.PartId));
    }
    
}

interface ICommand<TArgs, TData>
    where TArgs : ICommandArgs
    where TData : ICommandData
{
    TArgs Arguments { get; }
}
interface ICommandArgs {}
interface ICommandData {}

class AddCommand : ICommand<AddCommand.Args, AddCommand.Data> {
    public class Args : ICommandArgs {}
    class Data : ICommandData {}

    public Args Arguments { get; }
}
class ListCommand : ICommand<ListCommand.Args, ListCommand.Data> {
    public class Args : ICommandArgs {}
    public class Data : ICommandData {}
    
    public Args Arguments { get; init; }
}

class CSender
{
    public void doSomething()
    {
        SendCommand(new ListCommand{Arguments = new ListCommand.Args()});
        Console.WriteLine("doSomething");
    }
    TData SendCommand<TArgs, TData>(ICommand<TArgs, TData> command)
        where TArgs : ICommandArgs
        where TData : ICommandData
    {
        var args = command.Arguments;
        return default;
    }
}