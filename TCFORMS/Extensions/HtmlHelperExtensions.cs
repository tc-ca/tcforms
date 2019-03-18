using System.Web;
using System.Web.Mvc;

namespace TCFORMS.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString JsEscape(this HtmlHelper helper, string value)
        {
            return helper.Raw(value?.Replace("\\", "\\\\")?.Replace("'", "\\'"));
        }

        public static IHtmlString JsEscape(this HtmlHelper helper, object value)
        {
            return helper.JsEscape(value?.ToString());
        }
    }
}