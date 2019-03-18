namespace NTC.Domain
{
    using System.Data.Entity;
    using NTC.Domain.Entities;

    public partial class NTCContext : DbContext
    {
        public NTCContext()
            : base("name=NTCContext")
        {
        }

        public virtual DbSet<RoleEntity> Roles { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RemoteUserEntity> RemoteUsers { get; set; }
        public virtual DbSet<UserRoleEntity> UserRoles { get; set; }
        public virtual DbSet<UserMappingReferenceEntity> UserMappingReferences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RoleEntity>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.RoleCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.RoleLastUpdated)
                .WithRequired(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId)
                .WillCascadeOnDelete(false);

            // TODO: Figure out if the self-reference is still needed and/or why it breaks
            //modelBuilder.Entity<UserEntity>()
            //    .HasMany(e => e.UsersCreated)
            //    .WithOptional(e => e.UserCreated)
            //    .HasForeignKey(e => e.UserCreatedId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UsersLastUpdated)
                .WithOptional(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.RemoteUserCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.RemoteUserLastUpdated)
                .WithRequired(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserRoleUser)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserRoleCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserRoleLastUpdated)
                .WithRequired(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserMappingReferenceUser)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserMappingReferenceCreated)
                .WithRequired(e => e.UserCreated)
                .HasForeignKey(e => e.UserCreatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserMappingReferenceLastUpdated)
                .WithRequired(e => e.UserLastUpdated)
                .HasForeignKey(e => e.UserLastUpdateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RemoteUserEntity>()
                .Property(e => e.Identifier)
                .IsUnicode(false);

            modelBuilder.Entity<RemoteUserEntity>()
                .HasMany(e => e.UserMappingReferences)
                .WithRequired(e => e.RemoteUsers)
                .WillCascadeOnDelete(false);
        }
    }
}
