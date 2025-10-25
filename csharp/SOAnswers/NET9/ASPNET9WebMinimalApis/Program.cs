using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();
ILogger log = NullLoggerFactory.Instance.CreateLogger("test");

log.MyLogInformation($"tests {app}");
log.LogInformation($"tests {app}");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // app.UseDeveloperExceptionPage();
}

if (app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseWhen(ctx => ctx.Request.Host.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase),
        appBuilder => appBuilder.UseDeveloperExceptionPage());
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");
app.MapGet("/error", _ => throw new NotImplementedException("Hey"));

TestLocal("/error"); // no highlight
MapGet("/error"); // highlights

ShopRouter.MapGet("/error",_ => throw new NotImplementedException("Hey")); // highlights
ShopRouter.MapGet("/error"); // highlights
ShopRouter.MyName("/error",_ => throw new NotImplementedException("Hey"));  // highlights
new ShopRouter("/api/{controller}/{action}") // no highlight
    .Do("/api/{controller}/{action}"); // highlights

ShopRouter.Create("/api/{controller}/{action}"); // highlights

void TestLocal([StringSyntax("Route"), RouteTemplate] string pattern){}
void MapGet([StringSyntax("Route")] string pattern){}

class ShopRouter
{
    public ShopRouter([StringSyntax("Route")] string pattern) { }

    public void Do([StringSyntax("Route")] string pattern) => new ShopRouter(pattern);
    public static ShopRouter Create([StringSyntax("Route")] string qwer) => new ShopRouter(qwer);
    public static void MyName([StringSyntax("Route")] string pattern, RequestDelegate requestDelegate){}
    public static void MapGet([StringSyntax("Route")] string pattern, RequestDelegate requestDelegate){}
    public static void MapGet([StringSyntax("Route")] string pattern){}
}


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


