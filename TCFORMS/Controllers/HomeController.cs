using GoC.WebTemplate;
using Resources;
using System.Web.Mvc;

namespace TCFORMS.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Cdts
        [Route("home")]
        public ActionResult Index()
        {
            

            AddLinkToHomeMenu(new Link { Href = Url.Action("Index", "Form"), Text = Labels.NewOrExistingSubmission });
            AddBreadcrumb(Labels.ApplicationMenuHeader);

            return View();
        }
    }
}