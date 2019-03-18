using Resources;
using System.Web.Mvc;

namespace TCFORMS.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        [Route("error")]
        public ActionResult Index()
        {
            AddBreadcrumb(Labels.ApplicationMenuHeader);

            return View();
        }

        [Route("notfound")]
        public ViewResult NotFound()
        {
            AddBreadcrumb(Labels.ApplicationMenuHeader);

            Response.StatusCode = 404;

            return View("NotFound");
        }
    }
}