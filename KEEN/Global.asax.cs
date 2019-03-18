using KEEN.Handlers;
using log4net;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;


namespace KEEN
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected ILog Logger => LogManager.GetLogger(GetType());

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;

            // setup logging
            log4net.Config.XmlConfigurator.Configure();

            Logger.Info("Starting our engines....");
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.Info("Ending application...");
        }
    }
}
