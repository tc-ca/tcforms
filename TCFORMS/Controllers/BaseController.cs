using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoC.TC.Repositories;
using GoC.WebTemplate;
using log4net;
using NTC.Domain;
using Resources;
using TCFORMS.Extensions;
using TCFORMS.Services;
using TCMailer;
using TCWebAPI;

namespace TCFORMS.Controllers
{
    public class BaseController : WebTemplateBaseController
    {
        protected ILog Logger => LogManager.GetLogger(GetType());
        protected string Endpoint => System.Configuration.ConfigurationManager.AppSettings["ApiRoot"];

        protected readonly IAsyncRepository Repository;

        protected static string Language => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "fr" ? "fr-CA" : "en";

        protected int CurrentUser;

        private Client _apiClient;
        protected Client ApiClient => _apiClient ?? (_apiClient = new Client(Endpoint, Logger));

        protected SmtpClient InitializeEmailClient() => new SmtpClient();

        public BaseController()
        {
            WebTemplateCore.Environment = "AKAMAI";
            Repository = new EfRepository(new NTCContext());

        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // we call the base method to set the language
            var result = base.BeginExecuteCore(callback, state);

            WebTemplateCore.HeaderTitle = Labels.ApplicationMenuHeader;
            WebTemplateCore.ApplicationTitle.Text = Labels.ApplicationMenuHeader;
            WebTemplateCore.ApplicationTitle.Href = Url.Action("Index", "Home");
            WebTemplateCore.ShowSignOutLink = true;
            WebTemplateCore.SignOutLinkURL = Config.LogoutUrl;

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            WebTemplateCore.VersionIdentifier = version;

            // also show the environment when not in production
            if (Config.Environment != "PROD")
                WebTemplateCore.VersionIdentifier = $"{Config.Environment} - {version}";

            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb(Urls.CanadaHome, Labels.Home, ""));
            WebTemplateCore.LeftMenuItems.Add(new MenuSection
            {
                Name = Labels.HomeMenu,
                Items = new List<Link>
                {
                    new MenuItem { Href = Url.Action("Index", "Home"), Text = Labels.Home },
                    new MenuItem { Href = Urls.UserGuide, Text = Labels.UserGuide, OpenInNewWindow = true }
                }
            });

            return result;
        }

        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger.InfoFormat($"OnActionExecuting - Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} Action: {filterContext.ActionDescriptor.ActionName}");

            WebTemplateCore.LanguageLink.Href = TranslateUrl(filterContext);

            base.OnActionExecuting(filterContext);
        }

        [NonAction]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logger.InfoFormat($"OnActionExecuted - Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} Action: {filterContext.ActionDescriptor.ActionName}");
            base.OnActionExecuted(filterContext);
        }

        private static string TranslateUrl(ActionExecutingContext filterContext)
        {
            var lang = Language == "en" ? "fr-CA" : "en-CA";
            var culture = CultureInfo.CreateSpecificCulture(lang);

            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var builder = new UriBuilder(filterContext.HttpContext.Request.Url?.AbsoluteUri ?? "")
            {
                Path = culture.RunWithCulture(() => urlHelper.Action(filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName)) ?? ""
            };

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Set("GocTemplateCulture", lang);
            builder.Query = query.ToString();

            if (!string.IsNullOrEmpty(Config.LanguageToggleUrl))
            {
                var url = builder.Uri.AbsoluteUri;
                builder = new UriBuilder(Config.LanguageToggleUrl);
                query = HttpUtility.ParseQueryString(builder.Query);
                query.Set("_gc_lang", lang == "en-CA" ? "eng" : "fra");
                query.Set("ret", url);
                builder.Query = query.ToString();
                return builder.Uri.AbsoluteUri;
            }

            return builder.Uri.PathAndQuery;
        }

        protected async Task<int> GetCurrentNTCUser() => await UserServices.GetOrCreateUser(Repository, GetUserMbun());


        protected async Task<int> GetUserId()
        {
            var mbun = GetUserMbun();
            Session[Config.UserIdSessionKey] = (await ApiClient.GetOrCreateUser(Config.ProgramId, mbun, Language)).Response;

            return (int)Session[Config.UserIdSessionKey];
        }

        protected async Task<int> GetAdminUserId()
        {
            var mbun = GetUserMbun();
            var externalUserEntity = await UserServices.GetRemoteUserAsync(Repository, mbun);
            
            Session[Config.UserIdSessionKey] = externalUserEntity?.Id ?? 0;

            return (int)Session[Config.UserIdSessionKey];
        }

        protected async Task<bool> IsUserAdmin()
        {
            var mbun = GetUserMbun();
            return await UserServices.IsUserAdminAsync(Repository, mbun);
        }

        private string GetUserMbun()
        {
            var mbun = Config.SimulateMbun ? Config.Mbun : Request.ServerVariables[Config.MbunServerKey];
            var sessionMbun = Session[Config.MbunSessionKey]?.ToString();

            if (string.IsNullOrEmpty(sessionMbun) || sessionMbun != mbun || Session[Config.UserIdSessionKey] == null)
            {
                Session[Config.MbunSessionKey] = mbun;
            }

            return mbun;
        }


        protected void AddBreadcrumb(string title, string href = null)
        {
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb(href, title, ""));
        }

        protected void AddLinkToHomeMenu(Link link)
        {
            var items = WebTemplateCore.LeftMenuItems.First().Items;
            items.Insert(items.Count - 1, link);
        }
    }
}