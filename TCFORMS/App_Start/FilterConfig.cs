using System.Web.Mvc;

namespace TCFORMS
{
    public class FilterConfig
    {
        public static void Configure(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
