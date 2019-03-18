using RouteLocalization.Mvc;
using RouteLocalization.Mvc.Extensions;
using RouteLocalization.Mvc.Setup;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using TCFORMS.Controllers;

namespace TCFORMS
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes(Localization.LocalizationDirectRouteProvider);

            // Route Translations Provider Configuration & Registration
            routes.Localization(config =>
            {

                config.DefaultCulture = "en";
                config.AcceptedCultures = new HashSet<string>() { "en", "fr" };
                config.AttributeRouteProcessing = AttributeRouteProcessing.AddAsNeutralAndDefaultCultureRoute;
                config.AddTranslationToSimiliarUrls = true;

            }).TranslateInitialAttributeRoutes().Translate(localization =>
            {
                localization.RegisterRouteTranslations();
            });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public static class Routing
    {
        public static void RegisterRouteTranslations(this Localization localization)
        {
            #region Home
            localization.ForCulture("fr")
                .ForController<HomeController>()
                .ForAction(x => x.Index())
                .AddTranslation("accueil");
            #endregion

            #region Form
            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Index())
                .AddTranslation("formulaire");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Section(0), new[] { typeof(int) })
                .AddTranslation("formulaire/section/{id}");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Section(0, null), new[] { typeof(int), typeof(FormCollection) })
                .AddTranslation("formulaire/section/{id}");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Review())
                .AddTranslation("formulaire/revoir");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Submit(false))
                .AddTranslation("formulaire/soumettre");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Complete())
                .AddTranslation("formulaire/completer");

            localization.ForCulture("fr")
                .ForController<FormController>()
                .ForAction(x => x.Preview(0))
                .AddTranslation("formulaire/apercu/{id}");
            #endregion

            #region Error
            localization.ForCulture("fr")
                .ForController<ErrorController>()
                .ForAction(x => x.Index())
                .AddTranslation("erreur");

            localization.ForCulture("fr")
                .ForController<ErrorController>()
                .ForAction(x => x.NotFound())
                .AddTranslation("pastrouve");
            #endregion
        }
    }
}