using System;
using System.Threading.Tasks;
using GoC.TC.Repositories;
using NTC.Domain.Entities;

namespace TCFORMS.Services
{
    public static class UserServices
    {
        public static async Task<bool> IsUserAdminAsync(IAsyncRepository repo, string identifier)
        {
            var externalUser = await GetRemoteUserAsync(repo, identifier);

            if (externalUser == null) return false;
            
            var userRole = await repo.GetOneAsync<UserRoleEntity>(x => x.UserId == externalUser.Id && x.RoleId == Config.AdminRoleId);

            return userRole != null;
        }

        public static async Task<RemoteUserEntity> GetRemoteUserAsync(IAsyncRepository repo, string identifier)
        {
            return await repo.GetOneAsync<RemoteUserEntity>(x => x.Identifier.Equals(identifier));
        }

        public static async Task<int> GetOrCreateUser(IAsyncRepository repo, string identifier)
        {
            var userCreds = await repo.GetOneAsync<RemoteUserEntity>( 
               u => u.DateDeleted == null && u.Identifier == identifier
            );

            if (userCreds?.Id == null)
            {
                // create user because none currently exist with the mapping

                var user = await CreateUser(repo, identifier);

                return user.Id;

            }
            

            // user credentials exist; go find user associated.
            var userMapping = await repo.GetOneAsync<UserMappingReferenceEntity>(
                um => um.DateDeleted == null && um.RemoteUserId == userCreds.Id
            );

            return userMapping?.Id ?? 0;
            

        }

        public static async Task<UserEntity> CreateUser(IAsyncRepository repo, string identifier)
        {
            using (var trans = repo.BeginTransaction())
            {
                var user = new UserEntity
                {
                    Id = await repo.NextSequenceAsync<UserEntity>(),
                    DateCreated = DateTime.Now,
                    UserCreatedId = Config.SystemUserId
                };
                var returnUser = await repo.AddAsync(user);
                var remoteUser = new RemoteUserEntity
                {
                    Id = await repo.NextSequenceAsync<RemoteUserEntity>(),
                    Identifier = identifier,
                    DateCreated = DateTime.Now,
                    UserCreatedId = Config.SystemUserId
                };
                var returnRemoteUser = await repo.AddAsync(remoteUser);
                var userMapping = new UserMappingReferenceEntity
                {
                    Id = returnUser.Id,
                    RemoteUserId = returnRemoteUser.Id,
                    DateCreated = DateTime.Now,
                    UserCreatedId = Config.SystemUserId
                };
                var returnUserMapping = repo.AddAsync(userMapping);
                trans.Commit();

                return returnUser;
            }
        }
    }
}