using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace TCFORMS
{
    public class Global : System.Web.HttpApplication
    {
        protected ILog Logger => LogManager.GetLogger(GetType());

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.Configure(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // setup logging
            log4net.Config.XmlConfigurator.Configure();

            Logger.Info("Starting our engines...");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            if (ex == null) return;

            Logger.Error(ex.Message);
            //Trace.TraceError(ex.ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.InfoFormat("Ending application...");
        }
    }
}