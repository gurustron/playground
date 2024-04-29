using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
	private readonly IApiVersionDescriptionProvider provider;

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
	/// </summary>
	/// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
	public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider ) => this.provider = provider;

	/// <inheritdoc />
	public void Configure( SwaggerGenOptions options )
	{
		// add a swagger document for each discovered API version
		// note: you might choose to skip or document deprecated API versions differently
		foreach ( var description in provider.ApiVersionDescriptions )
		{
			options.SwaggerDoc( description.GroupName, CreateInfoForApiVersion( description ) );
		}
	}

	private static OpenApiInfo CreateInfoForApiVersion( ApiVersionDescription description )
	{
		var text = new StringBuilder( "An example application with OpenAPI, Swashbuckle, and API versioning." );
		var info = new OpenApiInfo()
		{
			Title = "Example API",
			Version = description.ApiVersion.ToString(),
		};

		info.Description = text.ToString();

		return info;
	}
}