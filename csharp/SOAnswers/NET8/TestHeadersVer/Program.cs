using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
	{
		options.ReportApiVersions = true;
		options.ApiVersionReader = new HeaderApiVersionReader("X-Api-Version");
	})
	.AddApiExplorer(options =>
	{
		options.ApiVersionParameterSource = new HeaderApiVersionReader("X-Api-Version");
		// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
		// note: the specified format code will format the version as "'v'major[.minor][-status]"
		// options.GroupNameFormat = "'v'VVV";

		// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
		// can also be used to control the format of the API version in route templates
		// options.SubstituteApiVersionInUrl = true;
	} )
	.EnableApiVersionBinding();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen( options => options.OperationFilter<SwaggerDefaultValues>() );
var app = builder.Build();

var versionSet = app.NewApiVersionSet()
	.HasApiVersion( new ApiVersion(1))
	.HasApiVersion( new ApiVersion(2))
	.ReportApiVersions()
	.Build();

var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
	{
		return "1.0";
		// var forecast = Enumerable.Range(1, 5).Select(index =>
		// 		new WeatherForecast
		// 		(
		// 			DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		// 			Random.Shared.Next(-20, 55),
		// 			summaries[Random.Shared.Next(summaries.Length)]
		// 		))
		// 	.ToArray();
		// return forecast;
	})
	.WithApiVersionSet(versionSet)
	.HasApiVersion(1)
	.MapToApiVersion(1)
	.WithOpenApi();

app.MapGet("/weatherforecast", () => "2.0")
	.WithApiVersionSet(versionSet)
	.HasApiVersion(2)
	.MapToApiVersion(2)
	.WithOpenApi();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		var descriptions = app.DescribeApiVersions();

		// build a swagger endpoint for each discovered API version
		foreach ( var description in descriptions )
		{
			var url = $"/swagger/{description.GroupName}/swagger.json";
			var name = description.GroupName.ToUpperInvariant();
			options.SwaggerEndpoint( url, name );
		}
	});
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class SwaggerDefaultValues : IOperationFilter
{
    /// <inheritdoc />
    public void Apply( OpenApiOperation operation, OperationFilterContext context )
    {
        var apiDescription = context.ApiDescription;

        operation.Deprecated |= apiDescription.IsDeprecated();

        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
        foreach ( var responseType in context.ApiDescription.SupportedResponseTypes )
        {
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/b7cf75e7905050305b115dd96640ddd6e74c7ac9/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/SwaggerGenerator.cs#L383-L387
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach ( var contentType in response.Content.Keys )
            {
                if ( !responseType.ApiResponseFormats.Any( x => x.MediaType == contentType ) )
                {
                    response.Content.Remove( contentType );
                }
            }
        }

        if ( operation.Parameters == null )
        {
            return;
        }
        
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
        // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
        foreach ( var parameter in operation.Parameters )
        {
            var description = apiDescription.ParameterDescriptions.First( p => p.Name == parameter.Name );

            if ( parameter.Description == null )
            {
                parameter.Description = description.ModelMetadata?.Description;
            }

            if ( parameter.Schema.Default == null &&
                 description.DefaultValue != null &&
                 description.DefaultValue is not DBNull &&
                 description.ModelMetadata is ModelMetadata modelMetadata )
            {
                // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                var json = JsonSerializer.Serialize( description.DefaultValue, modelMetadata.ModelType );
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson( json );
            }

            parameter.Required |= description.IsRequired;
        }
        operation.Parameters.Add(new OpenApiParameter
        {
	        In = ParameterLocation.Header,
	        Name = "X-Api-Version",
	        Example = new OpenApiString(apiDescription.GroupName)
        });
    }
}