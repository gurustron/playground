using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

/// <summary>
/// https://stackoverflow.com/a/79327706/2501279
/// </summary>
[MemoryDiagnoser]
public class SerilogBenchmark
{
    private static readonly ILogger<SerilogBenchmark> Logger;
    private static readonly string LogStringTemplate = "User {Username} {Email} has logged in.";
    private static Action<Microsoft.Extensions.Logging.ILogger, string, string, Exception?> LoggerAction =
        LoggerMessage.Define<string, string>(
            LogLevel.Information,
            new EventId(1, "Login"),
            LogStringTemplate);

    static SerilogBenchmark()
    {
        var services = new ServiceCollection();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.TextWriter(TextWriter.Null)
            // .Console(new CompactJsonFormatter())
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(dispose: true);
        });

        var serviceProvider = services.BuildServiceProvider();
        Logger = serviceProvider.GetRequiredService<ILogger<SerilogBenchmark>>();
    }

    [Benchmark]
    public void TestLogExtension()
    {
        Logger.LogInformation(new EventId(1, "Login"), LogStringTemplate, "User", "foo@bar.com");
    }

    [Benchmark]
    public void TestLogAction()
    {
        LoggerAction(Logger, "User", "foo@bar.com", null);
    }
}