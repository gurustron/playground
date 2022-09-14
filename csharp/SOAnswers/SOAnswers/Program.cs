﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Globalization;
using System.Reactive.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using OneOf;
using Moq;


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

