using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Security.Principal;
using ASPNET6Test;
using AutoMapper;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NET6LibTest;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPrincipal>(
    (sp) => sp.GetService<IHttpContextAccessor>()?.HttpContext?.User
);
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
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("x-correlation-id");
    logging.ResponseHeaders.Add("x-correlation-id");
});


var app = builder.Build();



app.Use(async (context, nextMiddleware) =>
{
    if (context.Response.Headers.ContainsKey("x-correlation-id"))
        context.Response.Headers["x-correlation-id"] = "test " + context.Request.Headers["x-correlation-id"];
    else
        context.Response.Headers.Add("x-correlation-id", "test " + context.Request.Headers["x-correlation-id"]);
    await nextMiddleware();
    var responseHeader = context.Response.Headers["x-correlation-id"];
});
app.UseHttpLogging();
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
// app.UseHttpsRedirection();


app.UseAuthorization();
app.UseMiddleware<TestMiddleware>();

app.MapGet("/sensor/sensor:{sensorId}/measurement", (int sensorId) =>
{
    return Results.Ok(sensorId);
});

app.MapPost("/query", async (HttpContext context, ILogger<Program> _) => await Fails1(context));
static async Task<string> Fails1(HttpContext context)
{
    using StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8);
    var query = await reader.ReadToEndAsync();
    var response = await Task.FromResult(query + " response");
    return response;
}
app.Map("Hello/{*rest}", (HttpRequest request, string rest) => $"Hello, World! Request: {request.Method} -> {rest}").WithName("Hello");
app.Map("/test1", (HttpRequest request, CancellationToken ct) => "`test1`").WithName("test1").WithDisplayName("asasdasd");
app.MapGet("/api/query-arr", (ArrayParser sizes) => sizes.Value);
app.MapGet("/test", () => Results.Ok("Hello World!"))
    // .RequireCustomAuth("TestMeta")
    ;
var query = new[]{new {StudentEmail = ""}}.GroupBy(
        k => k.StudentEmail,
        (baseEmail, emails) => new
        {
            Key = baseEmail,
            Count = emails.Count(),
        })
    .OrderByDescending(at => at.Count);
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

    public static Func<TIn?, TIn?, TIn?> Drop2<TIn>(Func<TIn, TIn, TIn> f) where TIn : struct
    {
        return (lhs, rhs) =>
        {
            if (lhs == null && rhs == null)
            {
                return default;
            }

            if (lhs == null && rhs != null)
            {
                return rhs;
            }

            if (rhs == null && lhs != null)
            {
                return lhs;
            }

            return f(lhs.Value, rhs.Value);
        };
    }
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

public interface IPrincipalProvider
{
    IPrincipal? Principal { get; }
}

public class PrincipalProvider : IPrincipalProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PrincipalProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public IPrincipal? Principal => _httpContextAccessor.HttpContext?.User;
}


    