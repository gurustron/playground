using System.Diagnostics;
using System.Reflection;
using ASPNET6Test;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NET6LibTest;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
builder.Services.AddSingleton<SomeLibraryClass>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfiles(new[] { new MappingProfile() });
});
;

// services.AddTransient<IFooService, AFooService>();
services.AddHttpClient<AFooService>((provider, client) => {client.BaseAddress = new Uri("https://localhost:44300");});
services.AddTransient<IFooService>(provider => provider.GetRequiredService<AFooService>());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.Use(async (context, next) =>
{
    Stopwatch watch = new Stopwatch();
    watch.Start();
    context.Response.OnStarting(() =>
    {
        watch.Stop();
        context.Response.Headers.Add("X-Response-Time", watch.ElapsedMilliseconds.ToString());
        return Task.CompletedTask;
    });
    await next(context);
    
});
var requiredService = app.Services.GetRequiredService<IEnumerable<IFooService>>();
var aFooService = app.Services.GetService<AFooService>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.UseAuthorization();
app.UseMiddleware<TestMiddleware>();

app.MapGet("/api/query-arr", ([FromQuery] ArrayParser sizes) => sizes.Value);
app.MapGet("/test", () => Results.Ok("Hello World!"))
    .RequireCustomAuth("TestMeta");


app.MapGet("/products/{id}", (GetProductByIdRequestDto request) => request);
app.MapControllers();

app.Run();

public enum SubscriberKind
{
    UserTrades
}

public class GetProductByIdRequestDto
{
    public string Id { get; set; }

    public static ValueTask<GetProductByIdRequestDto?> BindAsync(HttpContext context,
        ParameterInfo parameter)
    {
        const string idKey = "id";

        var result = new GetProductByIdRequestDto
        {
            Id = context.Request.RouteValues[idKey]?.ToString()
        };

        return ValueTask.FromResult<GetProductByIdRequestDto?>(result);
    }
}
public class ArrayParser
{
    public string[] Value { get; init; }

    public static bool TryParse(string? value, out ArrayParser result)
    {
        result = new()
        {
            Value = value?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()
        };

        return true;
    }
}
public class Test
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int I { get; set; }
}

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Test, Test>();
    }
}


public class TestMiddleware
{

    private readonly RequestDelegate _next;

    public TestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var someLibraryClass = context.Request.HttpContext.RequestServices.GetRequiredService<SomeLibraryClass>();
        if(context.GetEndpoint()?.Metadata.GetMetadata<SomeCustomMeta>() is{ } meta)
        {
            Console.WriteLine("SomeCustomMeta");
        }
        
        await _next(context);
    }
}

public class SomeCustomMeta
{
    public string Meta { get; set; }
}

public interface IFooService
{
}

public class AFooService : IFooService
{
    internal Uri? X { get; set; }

    public AFooService(HttpClient httpClient)
    {
        X = httpClient.BaseAddress;
    }
}
