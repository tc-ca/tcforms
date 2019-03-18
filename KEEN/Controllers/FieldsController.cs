using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
namespace KEEN.Controllers
{
    [RoutePrefix("api/programs/{programId}/users/{userId}/submissions/{submissionId}/fields")]
    public class FieldsController : BaseApiController
    {
        public FieldsController() { }
        public FieldsController(IRepository repository) : base(repository) { }

        [Route("{fieldType}")]
        public IList<Field> Get(int programId, int userId, int submissionId, string fieldType)
        {
            var submission = Repository.GetOne<SubmissionEntity>(
                s => s.DateDeleted == null && s.UserId == userId && s.Id == submissionId
            );
            var fields = submission.SubmissionSections
                .Where(s => s.DateDeleted == null)
                .Select(
                    s => s.FieldResponses
                        .Where(r => r.DateDeleted == null && r.Field.FieldTypeCode == fieldType)
                        .ToDictionary(r => r.Field.ToField(Languages), r => r.Text)
                )
                .SelectMany(l => l)
                .ToDictionary(x => x.Key, x => x.Value);
            fields.ForEach(kvp => kvp.Key.FillResponse(kvp.Value));
            return fields.Select(kvp => kvp.Key).ToList();
        }
    }
}