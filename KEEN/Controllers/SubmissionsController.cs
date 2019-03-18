using System.Web.Http;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;
using KEEN.Services;
using MoreLinq.Extensions;
using System;

namespace KEEN.Controllers
{
    [Authorize]
    [RoutePrefix("api/programs/{programId}/users/{userId}/submissions")]
    public class SubmissionsController : BaseApiController
    {
        public SubmissionsController() { }
        public SubmissionsController(IRepository repository) : base(repository) { }

        [Route("{submissionId}")]
        public Form Get(int programId, int userId, int submissionId)
        {
            var submission = Repository.GetOne<SubmissionEntity>(
                s => s.DateDeleted == null && s.UserId == userId && s.Id == submissionId
            );
            
            if (submission == null)
            {
                //send a forbidden error response back;
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden);
            }

            var form = submission.Form.ToForm(Languages, addFields: true);
            form.Sections.ForEach(s => s.FillValues(Repository, userId, submission.Id));

            
            return form;
        }

        [Route("current/{formId}")]
        public int? GetCurrent(int programId, int userId, int formId)
        {
            return SubmissionServices.GetCurrentSubmission(Repository, formId, userId)?.Id;
        }

        [HttpPut]
        [Route("{submissionId}/submit")]
        public void Put(int programId, int userId, int submissionId)
        {
            var submission = Repository.GetOne<SubmissionEntity>(
                s => s.DateDeleted == null && s.UserId == userId && s.Id == submissionId
            );
            SubmissionServices.CompleteSubmission(Repository, userId, submission);
        }
    }
}