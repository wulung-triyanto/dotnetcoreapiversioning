using API.Version.Sample.Filter;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;

namespace API.Version.Sample.Extension
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

                service.ApiVersionDescriptions.ToList().ForEach(api =>
                {
                    c.SwaggerDoc(api.GroupName, new OpenApiInfo
                    {
                        Title = "Sample REST API",
                        Description = "Sample REST API for API Versioning Proof of Concept"
                    });
                });
                c.DocumentFilter<SwaggerDocFilter>();

                string xmlDocFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlDocPath = Path.Combine(AppContext.BaseDirectory, xmlDocFile);
                c.IncludeXmlComments(xmlDocPath);
            });
        }

        public static void UseSwaggerApp(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                provider.ApiVersionDescriptions.ToList().ForEach(api =>
                {
                    x.SwaggerEndpoint($"/swagger/{api.GroupName}/swagger.json", $"API.Version.Sample.{api.GroupName}");
                });
                x.RoutePrefix = "swagger";
                x.DocumentTitle = "Sample REST API";
            });
        }
    }
}
