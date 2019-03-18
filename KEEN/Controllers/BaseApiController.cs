using System.Collections.Generic;
using log4net;
using System.Web.Http;
using KEEN.Domain;
using GoC.TC.Repositories;
using KEEN.Extensions;

namespace KEEN.Controllers
{
    public class BaseApiController : ApiController
    {
        protected ILog Logger => LogManager.GetLogger(GetType());
        protected IList<string> Languages => Request?.Headers?.ParseAcceptLanguageInfo();

        protected readonly IRepository Repository;

        public BaseApiController()
        {
            Repository = new EfRepository(new KeenContext());
        }

        public BaseApiController(IRepository repository)
        {
            Repository = repository;
        }
    }
}
