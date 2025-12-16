using System.ComponentModel.DataAnnotations;

using ASPNET10MinApis.Lib;

using Microsoft.Extensions.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
#pragma warning disable ASP0029
var types = typeof(TestRequest).Assembly.GetTypes()
    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableTo(typeof(IValidatableInfoResolver))))
    .Select(t => Activator.CreateInstance(t)).Cast<IValidatableInfoResolver>()
    .ToList();
builder.Services.AddValidation(options =>
{
    foreach (IValidatableInfoResolver resolver in types)
    {
        options.Resolvers.Add(resolver);
    }
});

#pragma warning restore ASP0029

builder.Services.AddValidationFromAssembly();
builder.Services.AddValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast");
app.MapPost("/test_same", (TestRequestCurrAss requestCurrAss) => TypedResults.Ok());
app.MapSampleEndpoint();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class TestRequestCurrAss
{
    [Required]
    public string? Id { get; set; }
}