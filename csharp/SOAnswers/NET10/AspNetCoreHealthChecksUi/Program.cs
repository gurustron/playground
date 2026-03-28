// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
//
// app.MapGet("/", () => "Hello World!");
//
// app.Run();

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var log = loggerFactory.CreateLogger<Program>();
IHost host = CreateHostBuilder(args, log).Build();
var t = new Thread(() =>
{
    host.Run();
}) {
    IsBackground = true
};
    t.Start();
    t.Join();

static IHostBuilder CreateHostBuilder(string[] args, ILogger log)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            string port = "18547";
            webBuilder.UseKestrel();
            webBuilder.UseUrls($"http://*:{port}");

            webBuilder.ConfigureServices((services) =>
            {
                string configString = "Host=localhost;Port=6432;Database=ui_healthchecks;Username=postgres;Password=P@ssword;";


                /// If I comment out these two AddHealthCheck calls,
                ///   the exception is not thrown (unsurprisingly).
                services.AddHealthChecks()
                    .AddCheck<RandomHealthCheck>("random")
                    // .AddCheck("MyAppHealth", new HealthCheck(myapp))
                    ;
                services.AddHealthChecksUI()
                    .AddPostgreSqlStorage(configString);

                services.AddControllers();
            });

            webBuilder.Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHealthChecks($"/myapp_health");
                    endpoints.MapHealthChecks($"/myapp_health_ui", new HealthCheckOptions
                    {
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                    endpoints.MapControllers();
                });
            });
        });
}

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (DateTime.UtcNow.Minute % 2 == 0)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }

        return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed", exception: new InvalidCastException("Invalid cast from to to to")));
    }
}

