using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using log4net.Config;
[assembly: OwinStartup(typeof(IdentityServerWeb.Startup))]

namespace IdentityServerWeb
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();
            var options = new IdentityServerOptions
            {
                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(new List<InMemoryUser>()),
                RequireSsl = false
            };
            app.UseIdentityServer(options);
        }
    }
}