using System.Linq;
using System.Web.Http;

namespace KEEN
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Remove default XML handlers
            var matches = config.Formatters
                .Where(f => f.SupportedMediaTypes.Any(
                    m => m.MediaType.ToString() == "application/xml" ||
                         m.MediaType.ToString() == "text/xml")
                )
                .ToList();

            foreach (var match in matches)
                config.Formatters.Remove(match);
        }
    }
}
