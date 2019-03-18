using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;
using MoreLinq.Extensions;
using KEEN.Services;

namespace KEEN.Controllers
{
    [RoutePrefix("api/programs")]
    public class ProgramsController : BaseApiController
    {
        public ProgramsController() { }
        public ProgramsController(IRepository repository) : base(repository) { }

        [Route("")]
        public IEnumerable<Program> Get()
        {
            return Repository
                .Get<ProgramEntity>(p => p.DateDeleted == null)
                .Select(p => p.ToProgram(Languages));
        }

        [Route("{programId}")]
        public Program Get(int programId)
        {
            return Repository
                .GetOne<ProgramEntity>(p => p.DateDeleted == null && p.Id == programId)
                .ToProgram(Languages);
        }

        [Authorize]
        [Route("{programId}/submissions/{submissionId}")]
        public Form Get(int programId, int submissionId)
        {
            var submission = Repository.GetOne<SubmissionEntity>(
                s => s.DateDeleted == null && s.Id == submissionId
            );
            var form = submission.Form.ToForm(Languages, addFields: true);
            form.Sections.ForEach(s => s.FillValues(Repository, submission.UserId, submission.Id));
            return form;
        }

    }
}
