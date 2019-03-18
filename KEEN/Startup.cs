using System.Linq;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using KEEN.IDSrv;
using System.Collections.Generic;
using IdentityServer3.Core.Services.InMemory;

[assembly: OwinStartup(typeof(KEEN.Startup))]

namespace KEEN
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(new List<InMemoryUser>()),
                RequireSsl = false
            };
            //app.UseIdentityServer(options);

            app.Map("/core",
                coreApp =>
                {
                    coreApp.UseIdentityServer(options);
                }
            );


            app.UseErrorPage();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = IDSrv.Config.IdSrvBaseUrl,
                ValidationMode = ValidationMode.ValidationEndpoint,
                AuthenticationType = "Bearer",
                RequiredScopes = new[] { "keen" }
            });
            var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Filters.Add(new AuthorizeAttribute());
            // Remove default XML handlers
            var matches = config.Formatters
                .Where(f => f.SupportedMediaTypes.Any(
                    m => m.MediaType.ToString() == "application/xml" ||
                         m.MediaType.ToString() == "text/xml")
                )
                .ToList();
            foreach (var match in matches)
                config.Formatters.Remove(match);
            app.UseWebApi(config);
        }
    }
}
