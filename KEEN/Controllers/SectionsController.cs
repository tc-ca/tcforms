using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;
using KEEN.Services;

namespace KEEN.Controllers
{
    [RoutePrefix("api/programs/{programId}/forms/{formId}/sections")]
    public class SectionsController : BaseApiController
    {
        public SectionsController() { }
        public SectionsController(IRepository repository) : base(repository) { }

        [Route("")]
        public IList<Section> Get(int programId, int formId, bool addFields = false, int? userId = null)
        {
            var sections = Repository
                .Get<SectionEntity>(s => s.DateDeleted == null && s.FormId == formId)
                .OrderBy(s => s.DisplaySort)
                .Select(s => s.ToSection(Languages, addFields))
                .ToList();
            if (userId != null)
            {
                if (!UserServices.UserInProgram(Repository, userId.Value, programId))
                {
                    throw new InvalidOperationException($"The user {userId} does not exist in the program {programId}");
                }

                var submission = SubmissionServices.GetCurrentSubmission(Repository, formId, userId.Value);
                if (submission != null)
                {
                    sections.ForEach(s => s.FillValues(Repository, userId.Value, submission.Id));
                }
            }
            return sections;
        }

        [Route("{sectionId}")]
        public Section Get(int programId, int formId, int sectionId, int? userId = null)
        {
            var section = Repository
                .GetOne<SectionEntity>(s => s.DateDeleted == null && s.Id == sectionId)
                .ToSection(Languages);
            if (userId != null)
            {
                if (!UserServices.UserInProgram(Repository, userId.Value, programId))
                {
                    throw new InvalidOperationException($"The user {userId} does not exist in the program {programId}");
                }

                var submission = SubmissionServices.GetCurrentSubmission(Repository, formId, userId.Value);
                if (submission != null)
                {
                    section.FillValues(Repository, userId.Value, submission.Id);
                }
            }
            return section;
        }

        [HttpPut]
        [Route("{sectionId}/save")]
        public void Put(int programId, int formId, int sectionId, int userId, [FromBody] IDictionary<int, string> fields)
        {
            if (!UserServices.UserInProgram(Repository, userId, programId))
            {
                throw new InvalidOperationException($"The user {userId} does not exist in the program {programId}");
            }

            SectionServices.SaveSection(Repository, formId, sectionId, userId, fields);
        }
    }
}
