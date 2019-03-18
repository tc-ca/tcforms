using System.Data.Entity;
using KEEN.Domain.Entities;

namespace KEEN.Domain
{
    public class KeenContext : DbContext
    {
        public KeenContext()
            : base("name=KeenContext")
        {
        }

        public virtual DbSet<LanguageEntity> Languages { get; set; }
        public virtual DbSet<LabelEntity> Labels { get; set; }
        public virtual DbSet<ResourceEntity> Resources { get; set; }
        public virtual DbSet<FieldTypeEntity> FieldTypes { get; set; }
        public virtual DbSet<ValidationAttributeEntity> ValidationAttributes { get; set; }
        public virtual DbSet<ValidationOperatorEntity> ValidationOperators { get; set; }
        public virtual DbSet<FieldSetEntity> FieldSets { get; set; }
        public virtual DbSet<FieldSetValueEntity> FieldSetValues { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ProgramEntity> Programs { get; set; }
        public virtual DbSet<TimeWindowEntity> TimeWindows { get; set; }
        public virtual DbSet<FormEntity> Forms { get; set; }
        public virtual DbSet<SectionEntity> Sections { get; set; }
        public virtual DbSet<FieldEntity> Fields { get; set; }
        public virtual DbSet<ValidationEntity> Validations { get; set; }
        public virtual DbSet<ValidationRuleEntity> ValidationRules { get; set; }
        public virtual DbSet<FieldValidationEntity> FieldValidations { get; set; }
        public virtual DbSet<SectionFieldEntity> SectionFields { get; set; }
        public virtual DbSet<DocumentEntity> Documents { get; set; }
        public virtual DbSet<DocumentDataEntity> DocumentDatas { get; set; }
        public virtual DbSet<FieldResponseEntity> FieldResponses { get; set; }
        public virtual DbSet<UserProgramEntity> UserPrograms { get; set; }
        public virtual DbSet<SubmissionEntity> Submissions { get; set; }
        public virtual DbSet<SubmissionSectionEntity> SubmissionSections { get; set; }
        public virtual DbSet<FieldSetResponseEntity> FieldSetResponses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageEntity>()
                .Property(e => e.LanguageTag)
                .IsUnicode(false);

            modelBuilder.Entity<LanguageEntity>()
                .HasMany(e => e.Labels)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.Labels)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.FieldTypes)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.ValidationAttributes)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.ValidationOperators)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.FieldSetValues)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.Programs)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.Forms)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.Sections)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.Resource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.AdminValidations)
                .WithRequired(e => e.AdminResource)
                .HasForeignKey(e => e.AdminResourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.ErrorValidations)
                .WithRequired(e => e.ErrorResource)
                .HasForeignKey(e => e.ErrorResourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceEntity>()
                .HasMany(e => e.ErrorMessageValidations)
                .WithRequired(e => e.ErrorMessageResource)
                .HasForeignKey(e => e.ErrorMessageResourceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldTypeEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<FieldTypeEntity>()
                .HasMany(e => e.Fields)
                .WithRequired(e => e.FieldType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValidationAttributeEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ValidationAttributeEntity>()
                .HasMany(e => e.ValidationRules)
                .WithRequired(e => e.ValidationAttribute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValidationOperatorEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ValidationOperatorEntity>()
                .HasMany(e => e.ValidationRules)
                .WithRequired(e => e.ValidationOperator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldSetEntity>()
                .HasMany(e => e.Values)
                .WithRequired(e => e.FieldSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldSetEntity>()
                .HasMany(e => e.FieldSetResponses)
                .WithRequired(e => e.FieldSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldSetValueEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<FieldSetValueEntity>()
                .HasMany(e => e.FieldSetResponses)
                .WithRequired(e => e.FieldSetValue)
                .HasForeignKey(e => new { e.FieldSetId, e.FieldSetValueCd })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.LanguagesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.LanguagesLastUpdated)
                .WithRequired(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.LabelsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.LabelsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ResourcesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ResourcesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldTypesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldTypesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationAttributesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationAttributesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationOperatorsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationOperatorsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetValuesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetValuesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UsersCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UsersLastUpdated)
                .WithOptional(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ProgramsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ProgramsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.TimeWindowsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.TimeWindowsLastUpdated)
                .WithOptional(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FormsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FormsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SectionsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SectionsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationRulesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.ValidationRulesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldValidationsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldValidationsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SectionFieldsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SectionFieldsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.DocumentsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.DocumentsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.DocumentDatasCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.DocumentDatasLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldResponsesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldResponsesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserProgramsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserPrograms)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserProgramsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SubmissionsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Submissions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SubmissionsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SubmissionSectionsCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.SubmissionSectionsLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetResponsesCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.FieldSetResponsesLastUpdated)
                .WithOptional(e => e.UserLastUpdate)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<ProgramEntity>()
                .HasMany(e => e.Forms)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramEntity>()
                .HasMany(e => e.UserPrograms)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProgramEntity>()
                .Property(e => e.IdentifierText)
                .IsUnicode(false);

            modelBuilder.Entity<TimeWindowEntity>()
                .HasMany(e => e.Forms)
                .WithOptional(e => e.ActiveTimeWindow)
                .HasForeignKey(e => e.ActiveTimeWindowId);

            modelBuilder.Entity<FormEntity>()
                .HasMany(e => e.Sections)
                .WithRequired(e => e.Form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FormEntity>()
                .HasMany(e => e.Submissions)
                .WithRequired(e => e.Form)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SectionEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<SectionEntity>()
                .HasMany(e => e.SectionFields)
                .WithRequired(e => e.Section)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SectionEntity>()
                .HasMany(e => e.SubmissionSections)
                .WithRequired(e => e.Section)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SectionEntity>()
                .HasMany(e => e.FieldSetResponses)
                .WithRequired(e => e.Section)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldEntity>()
                .Property(e => e.FieldTypeCode)
                .IsUnicode(false);

            modelBuilder.Entity<FieldEntity>()
                .Property(e => e.CssClasses)
                .IsUnicode(false);

            modelBuilder.Entity<FieldEntity>()
                .HasMany(e => e.FieldValidations)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldEntity>()
                .HasMany(e => e.SectionFields)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldEntity>()
                .HasMany(e => e.FieldResponses)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldEntity>()
                .HasMany(e => e.FieldSetResponses)
                .WithRequired(e => e.Field)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValidationEntity>()
                .HasMany(e => e.ValidationRules)
                .WithRequired(e => e.Validation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValidationEntity>()
                .HasMany(e => e.FieldValidations)
                .WithRequired(e => e.Validation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ValidationRuleEntity>()
                .Property(e => e.ValidationAttributeCode)
                .IsUnicode(false);

            modelBuilder.Entity<ValidationRuleEntity>()
                .Property(e => e.ValidationOperatorCode)
                .IsUnicode(false);

            modelBuilder.Entity<ValidationRuleEntity>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentEntity>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentDataEntity>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentData)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubmissionEntity>()
                .HasMany(e => e.SubmissionSections)
                .WithRequired(e => e.Submission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubmissionEntity>()
                .HasMany(e => e.FieldSetResponses)
                .WithRequired(e => e.Submission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubmissionSectionEntity>()
                .HasMany(e => e.FieldResponses)
                .WithRequired(e => e.SubmissionSection)
                .HasForeignKey(e => new { e.SubmissionId, e.SectionId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FieldSetResponseEntity>()
                .Property(e => e.FieldSetValueCd)
                .IsUnicode(false);
        }
    }
}
