using System;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;

namespace KEEN.Services
{
    public static class UserServices
    {
        public static bool UserInProgram(IRepository repo, int userId, int programId)
            => repo.GetOne<UserProgramEntity>(up => up.DateDeleted == null && up.UserId == userId && up.ProgramId == programId) != null;

        public static UserEntity CreateUser(IRepository repo, int programId, string identifier)
        {
            using (var trans = repo.BeginTransaction())
            {
                var user = new UserEntity
                {
                    Id = repo.NextSequence<UserEntity>(),
                    DateCreated = DateTime.Now,
                    UserCreatedId = Config.SystemUserId
                };
                repo.Add(user);
                repo.Add(new UserProgramEntity
                {
                    UserId = user.Id,
                    ProgramId = programId,
                    IdentifierText = identifier,
                    DateCreated = DateTime.Now,
                    UserCreatedId = Config.SystemUserId
                });
                trans.Commit();
                return user;
            }
        }
    }
}