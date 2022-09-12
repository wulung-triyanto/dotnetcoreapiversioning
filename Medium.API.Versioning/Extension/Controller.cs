using Microsoft.AspNetCore.Mvc;

namespace API.Version.Sample.Extension
{
    public static class ControllerExtension
    {
        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = ApiVersion.Default;
                o.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'V";
                o.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
