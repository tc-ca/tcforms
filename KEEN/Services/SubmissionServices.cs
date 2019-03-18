using System;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;

namespace KEEN.Services
{
    public static class SubmissionServices
    {
        public static SubmissionEntity GetCurrentSubmission(IRepository repo, int formId, int userId)
            => repo.GetOne<SubmissionEntity>(s => s.DateDeleted == null && s.FormId == formId && s.UserId == userId && !s.IsComplete);

        public static void CompleteSubmission(IRepository repo, int userId, SubmissionEntity submission)
        {
            using (var trans = repo.BeginTransaction())
            {
                submission.IsComplete = true;
                submission.DateLastUpdate = DateTime.Now;
                submission.UserLastUpdateId = userId;
                repo.Update(submission);
                trans.Commit();
            }
        }
    }
}