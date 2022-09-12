using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.RegularExpressions;

namespace API.Version.Sample.Filter
{
    public class SwaggerDocFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SwaggerDocFilter(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var request = _contextAccessor.HttpContext?.Request;

            // we need to modify the Key, but that is read-only so let's just make a copy of the Paths property
            var copy = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                var newKey = Regex.Replace(path.Key, "/v[^/]*", string.Empty);
                newKey = newKey.ToLower();
                copy.Add(newKey, path.Value);
            }
            swaggerDoc.Paths.Clear();
            swaggerDoc.Paths = copy;
        }
    }
}
