#pragma warning disable 1591

using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _descriptionProvider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider descriptionProvider)
    {
        _descriptionProvider = descriptionProvider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo()
                {
                    Title = $"API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    // Description = , TermsOfService = , Contact = , License = 
                }
            );
        }

        // use fqn for dto descriptoins
        options.CustomSchemaIds(t => t.FullName);

        // Include docs from current API assembly (as described in MS Docs)
        var executingAssembly = Assembly.GetExecutingAssembly();
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{executingAssembly.GetName().Name}.xml"));

        // Additionally include the documentation of all other "relevant" projects
        var referencedProjectsXmlDocPaths = executingAssembly.GetReferencedAssemblies()
            .Where(assembly => assembly.Name != null && assembly.Name.StartsWith("Public.DTO", StringComparison.InvariantCultureIgnoreCase))
            .Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml"))
            .Where(path => File.Exists(path));
        foreach (var xmlDocPath in referencedProjectsXmlDocPaths)
        {
            options.IncludeXmlComments(xmlDocPath);
        }
        
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = 
                "foo bar",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}