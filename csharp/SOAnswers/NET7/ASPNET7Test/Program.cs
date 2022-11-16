using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using ASPNET7Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prometheus;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);
var eName = builder.Environment.EnvironmentName;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapMetrics();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/query-arr", (ArrayParser sizes) => sizes.Value);

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
