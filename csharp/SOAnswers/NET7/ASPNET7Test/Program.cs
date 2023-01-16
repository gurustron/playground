using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks.Dataflow;
using ASPNET7Test;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Options;
using Prometheus;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var services = builder.Services;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOutputCache();
builder.Services.AddRequestLocalization(options => options.DefaultRequestCulture = new RequestCulture("en-GB"));
builder.Services.AddValidatorsFromAssemblyContaining<ExampleValidator>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();
app.UseOutputCache();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapMetrics();

// app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseRequestLocalization(new RequestLocalizationOptions());
app.UseAuthorization();
app.UseSession();
app.MapControllers();



app.MapGet("test_uri", (HttpContext context) => context.Request.GetDisplayUrl());
app.MapGet("/api/test-cache", () => DateTime.UtcNow.ToString("O")).CacheOutput();
app.MapGet("/api/query-arr", (ArrayParser sizes) => sizes.Value);
    
app.MapPost("api/user", (Example e) =>  e )
    .AddEndpointFilter<ValidationFilter<Example>>()
    .WithMetadata(new RuleSetMetadata<Example>("Test"))
    ;
app.MapGet("/supplier/listen2", () =>
{
   
    bool endpointCalled = false;
    HttpListener listener = new HttpListener();

    listener.Prefixes.Add("http://localhost:7195/testi2/");
    listener.Start();

    listener.BeginGetContext(new AsyncCallback(Endpoint2), listener);
    var timer = new System.Timers.Timer(20000);
    timer.Elapsed += CloseEndpoint;
    timer.Start();

    void Endpoint2(IAsyncResult result)
    {
        var listener = (HttpListener)result.AsyncState;
        var context = listener.EndGetContext(result);
        var request = context.Request;
        var response = context.Response;

        string responseString = "<HTML><BODY> Hello from Endpoint2!</BODY></HTML>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();

        endpointCalled = true;
        listener.BeginGetContext(Endpoint2, listener);
    }
    
    void CloseEndpoint(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (!endpointCalled)
        {
            listener.Stop();
        }
    }

    return Results.Ok(new { message = "Endpoint2 created" });
});

app.MapControllers();
app.Run();

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
    
    TransformBlock<TIn, TOut> MakeTransformBlock<TIn, TOut>( Func<TIn, TOut> f )
    {
        return new TransformBlock<TIn, TOut>( f );
    }

    void TestIt( )
    {
        var d = StringToUpper;

        var block2 = MakeTransformBlock<string, string>( StringToUpper );
        var block3 = MakeTransformBlock( d );
    }

    static string StringToUpper( string input ) => input.ToUpper( );
}

class MyClass
{
    public T Do<T>(T l , T r) where T : INumber<T>
    {
        if (l > r)
        {
            return l;
        }

        return r;
    }

    public T Tets<T>(T x) where T : INumber<T> => x > T.Zero ? x : T.Zero;
    
    public T Max<T>(T v1, T v2) where T: IComparisonOperators<T, T, bool> => v1 > v2 ? v1 : v2;
}

public class ExampleValidator : AbstractValidator<Example> 
{
    public ExampleValidator() 
    {
        RuleSet("Test", () => 
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .Must(example => example.Contains("Test"));
        });

        RuleFor(example => example.Value)
            .NotEmpty();
    }
}
public class Example
{
    public string Value { get; set; }
}

public class RuleSetMetadata<T>
{
    public RuleSetMetadata(string ruleSet)
    {
        RuleSet = ruleSet;
    }

    public string RuleSet { get; set; }
}

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestBodyReader = context.HttpContext.Request.BodyReader;
        var atLeastAsync = await requestBodyReader.ReadAtLeastAsync(111, context.HttpContext.RequestAborted);
        requestBodyReader.AdvanceTo(atLeastAsync.Buffer.Start, atLeastAsync.Buffer.End);
        string? ruleSet = null;
        if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<RuleSetMetadata<T>>() is {} meta)
        {
            ruleSet = meta.RuleSet;
        }
        var obj = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (obj is null)
        {
            return Results.BadRequest();
        }

        var validationResult = ruleSet is null
            ? await _validator.ValidateAsync(obj)
            : await _validator.ValidateAsync(obj, options => options.IncludeRuleSets(ruleSet));

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(string.Join("/n", validationResult.Errors));
        }

        return await next(context);
    }
}

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{
}


class Fraction<T> :
    IAdditionOperators<Fraction<T>, Fraction<T>, Fraction<T>>,
    ISubtractionOperators<Fraction<T>, Fraction<T>, Fraction<T>>,
    IDivisionOperators<Fraction<T>, Fraction<T>, Fraction<T>>
    where T : INumber<T>
{
    public T Numerator { get; }
    public T Denominator { get; }

    public Fraction(T numerator, T denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public static Fraction<T> operator +(Fraction<T> left, Fraction<T> right) =>
        new(left.Numerator * right.Denominator + right.Numerator * left.Denominator,
            left.Denominator * right.Denominator);

    public static Fraction<T> operator -(Fraction<T> left, Fraction<T> right) =>
        new(left.Numerator * right.Denominator - right.Numerator * left.Denominator,
            left.Denominator * right.Denominator);

    public static Fraction<T> operator /(Fraction<T> left, Fraction<T> right) =>
        new(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
}
