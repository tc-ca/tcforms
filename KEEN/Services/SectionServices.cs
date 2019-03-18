using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Entities.Models;
using KEEN.Extensions;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KEEN.Services
{
    public static class SectionServices
    {
        public static bool SectionInForm(IRepository repo, int sectionId, int formId)
            => repo.GetOne<SectionEntity>(s => s.DateDeleted == null && s.Id == sectionId && s.FormId == formId) != null;

        public static bool FieldInSection(IRepository repo, int fieldId, int sectionId)
            => repo.GetOne<SectionFieldEntity>(sf => sf.DateDeleted == null && sf.SectionId == sectionId && sf.FieldId == fieldId) != null;

        public static void SaveSection(IRepository repo, int formId, int sectionId, int userId, IDictionary<int, string> fields)
        {
            if (!SectionInForm(repo, sectionId, formId))
            {
                throw new InvalidOperationException($"The section {sectionId} does not exist in the form {formId}");
            }

            using (var trans = repo.BeginTransaction())
            {
                var submission = SubmissionServices.GetCurrentSubmission(repo, formId, userId);
                if (submission == null)
                {
                    submission = new SubmissionEntity
                    {
                        Id = repo.NextSequence<SubmissionEntity>(),
                        FormId = formId,
                        UserId = userId,
                        IsComplete = false,
                        DateCreated = DateTime.Now,
                        UserCreatedId = userId
                    };

                    repo.Add(submission);
                }

                var section = submission.SubmissionSections.FirstOrDefault(s => s.SectionId == sectionId);
                if (section == null)
                {
                    section = new SubmissionSectionEntity
                    {
                        SubmissionId = submission.Id,
                        SectionId = sectionId,
                        IsComplete = false,
                        DateCreated = DateTime.Now,
                        UserCreatedId = userId
                    };
                    repo.Add(section);
                }
                else if (section.DateDeleted != null)
                {
                    section.FieldResponses.Where(r => r.DateDeleted == null).ForEach(r =>
                    {
                        r.DateDeleted = DateTime.Now;
                        r.DateLastUpdate = DateTime.Now;
                        r.UserLastUpdateId = userId;
                        repo.Update(r);
                    });
                    section.DateDeleted = null;
                    section.DateLastUpdate = DateTime.Now;
                    section.UserLastUpdateId = userId;
                    repo.Update(section);
                }

                if (fields.Any(f => !FieldInSection(repo, f.Key, sectionId)))
                {
                    throw new InvalidOperationException($"Some of the given fields do not exist in section {sectionId}");
                }

                fields.ForEach(f =>
                {
                    var entity = section.FieldResponses.FirstOrDefault(r => r.FieldId == f.Key);
                    if (entity == null)
                    {
                        entity = new FieldResponseEntity
                        {
                            SubmissionId = submission.Id,
                            SectionId = sectionId,
                            FieldId = f.Key,
                            Text = f.Value,
                            DateCreated = DateTime.Now,
                            UserCreatedId = userId
                        };
                        repo.Add(entity);
                    }
                    else
                    {
                        entity.Text = f.Value;
                        entity.DateDeleted = null;
                        entity.DateLastUpdate = DateTime.Now;
                        entity.UserLastUpdateId = userId;
                        repo.Update(entity);
                    }
                });

                trans.Commit();

                SaveFieldResponse(repo, formId, sectionId, userId, fields);
            }
        }

        public static void SaveFieldResponse(IRepository repo, int formId, int sectionId, int userId, IDictionary<int, string> fields)
        {
            using (var trans = repo.BeginTransaction())
            {
                var submissionId = SubmissionServices.GetCurrentSubmission(repo, formId, userId)?.Id ?? repo.NextSequence<SubmissionEntity>();

                //loop through responses
                foreach (var field in fields)
                {
                    var fieldId = field.Key;
                    var response = field.Value;
                    var fieldSet = repo.GetOne<FieldEntity>(x => x.Id == fieldId);

                    //should only apply to checboxes
                    if (fieldSet != null && !fieldSet.FieldTypeCode.Equals("checkbox")) continue;

                    var fieldSetId = fieldSet.FieldSet.Id;
                    var fieldSetResponses = new List<FieldSetResponseEntity>();

                    //all current fieldsetvalues in the db
                    var fieldsetvalues = repo.Get<FieldSetValueEntity>(x => x.FieldSetId == fieldSetId).ToList();

                    //answers
                    var selectedValues = response.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => new FieldSetResponseEntity
                        {
                            SubmissionId = submissionId,
                            SectionId = sectionId,
                            FieldId = fieldId,
                            FieldSetId = fieldSetId,
                            FieldSetValueCd = x,
                            SelectedInd = 1
                        }).ToList();

                    //missing one -> not answered.
                    var unselectedValues = fieldsetvalues
                        .Where(x => !selectedValues.Any(r => r.FieldSetValueCd.Equals(x.Code)))
                        .Select(x => new FieldSetResponseEntity
                        {
                            SubmissionId = submissionId,
                            SectionId = sectionId,
                            FieldId = fieldId,
                            FieldSetId = fieldSetId,
                            FieldSetValueCd = x.Code,
                            SelectedInd = 0
                        }).ToList();

                    fieldSetResponses.AddRange(selectedValues);
                    fieldSetResponses.AddRange(unselectedValues);

                    //order them by codes
                    fieldSetResponses = fieldSetResponses.OrderBy(x => x.FieldSetValueCd).ToList();

                    //upsert each values
                    foreach (var entity in fieldSetResponses)
                    {
                        var fieldSetResponse = repo.GetOne<FieldSetResponseEntity>(x => x.SubmissionId == submissionId &&
                                                                                        x.SectionId == entity.SectionId &&
                                                                                        x.FieldId == entity.FieldId &&
                                                                                        x.FieldSetId == entity.FieldSetId &&
                                                                                        x.FieldSetValueCd.Equals(entity.FieldSetValueCd));
                        if (fieldSetResponse == null)
                        {
                            fieldSetResponse = new FieldSetResponseEntity()
                            {
                                SubmissionId = entity.SubmissionId,
                                SectionId = entity.SectionId,
                                FieldId = entity.FieldId,
                                FieldSetId = entity.FieldSetId,
                                FieldSetValueCd = entity.FieldSetValueCd,
                                SelectedInd = entity.SelectedInd,
                                DateCreated = DateTime.Now,
                                UserCreatedId = userId
                            };

                            repo.Add(fieldSetResponse);
                        }
                        else
                        {
                            fieldSetResponse.SelectedInd = entity.SelectedInd;
                            fieldSetResponse.DateLastUpdated = DateTime.Now;
                            fieldSetResponse.DateDeleted = null;
                            fieldSetResponse.UserLastUpdateId = userId;

                            repo.Update(fieldSetResponse);
                        }
                    }
                }

                trans.Commit();
            }
        }

        public static void FillValues(this Section section, IRepository repo, int userId, int submissionId)
        {
            var submissionSection = repo.GetOne<SubmissionSectionEntity>(
                s => s.DateDeleted == null && s.SectionId == section.Id && s.SubmissionId == submissionId
            );
            if (submissionSection == null) return;
            section.Fields.ForEach(f =>
            {
                var response = submissionSection.FieldResponses.FirstOrDefault(r => r.FieldId == f.Id);
                if (!string.IsNullOrEmpty(response?.Text))
                {
                    f.FillResponse(response.Text);
                }
            });
        }
    }
}