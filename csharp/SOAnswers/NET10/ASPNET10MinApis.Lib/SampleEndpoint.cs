using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Validation;

namespace ASPNET10MinApis.Lib;

public static class SampleEndpoint
{
    public static IServiceCollection AddValidationFromAssembly(this IServiceCollection services)
    {
        services.AddValidation();
        return services;
    }
    public static void MapSampleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/test", (TestRequest request) => TypedResults.Ok());
    }
}

public class TestRequest
{
    [Required]
    public string? Id { get; set; }
}