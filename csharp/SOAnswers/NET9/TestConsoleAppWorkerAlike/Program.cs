using System.Diagnostics;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using TestConsoleAppWorkerAlike;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

var list = new List<int>{1,2,3,4,5,6,7};
var span = CollectionsMarshal.AsSpan(list);
Random.Shared.Shuffle(span);

// Random.Shared.GetItems()
Console.WriteLine(string.Join(",", list));


// var st = ExceptionDispatchInfo.Capture(new Exception("test"));
// Console.WriteLine(st.SourceException.StackTrace);
// Do();
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// var host = builder.Build();
// host.Run();


void Do()
{
    var ex = new Exception("test");
    ExceptionDispatchInfo.SetRemoteStackTrace(ex, "here");
    throw new Exception("test");
}

public partial class Program
{
    public static void Func<T>(T val, T? _ = default) where T : INumber<T>
    {
        System.Console.WriteLine("num");
    }

    public static void Func<T>(T val)
    {

        System.Console.WriteLine(typeof(T));
    }
} 



static class AsyncLogger
{
    private static readonly BlockingCollection<string> _logQueue = new();
    private static readonly Task _loggingTask;

    static AsyncLogger()
    {
        _loggingTask = Task.Run(async () =>
        {
            foreach (var msg in _logQueue.GetConsumingEnumerable())
            {
                await Console.Out.WriteLineAsync(msg);
            }
        });
    }

    public static void Log(string message)
    {
        // Don’t block — drop message if queue is too full
        if (_logQueue.Count < 10_000)
            _logQueue.Add(message);
    }

    public static async Task FlushAsync()
    {
        // Wait until all messages processed
        _logQueue.CompleteAdding();
        await _loggingTask;
    }
}