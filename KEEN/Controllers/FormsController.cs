using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;

namespace KEEN.Controllers
{
    [RoutePrefix("api/programs")]
    public class FormsController : BaseApiController
    {
        public FormsController() { }
        public FormsController(IRepository repository) : base(repository) { }

        [Route("{programId}/forms")]
        public IEnumerable<Form> Get(int programId)
        {
            return Repository
                .Get<FormEntity>(f => f.DateDeleted == null && f.ProgramId == programId)
                .Select(f => f.ToForm(Languages));
        }

        [Route("{programId}/forms/{formId}")]
        public Form Get(int programId, int formId)
        {
            return Repository
                .GetOne<FormEntity>(f => f.DateDeleted == null && f.Id == formId)
                .ToForm(Languages);
        }
    }
}