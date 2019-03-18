using System.Web.Http;
using KEEN.Domain.Entities;
using GoC.TC.Repositories;
using KEEN.Services;

namespace KEEN.Controllers
{
    [RoutePrefix("api/programs/{programId}/users")]
    public class UsersController : BaseApiController
    {
        public UsersController() { }
        public UsersController(IRepository repository) : base(repository) { }

        [HttpPost]
        [Route("create")]
        public int Post(int programId, [FromBody] string identifier)
        {
            var userProgram = Repository.GetOne<UserProgramEntity>(
                up => up.DateDeleted == null && up.ProgramId == programId && up.IdentifierText == identifier
            );
            return userProgram?.UserId ?? UserServices.CreateUser(Repository, programId, identifier).Id;
        }
    }
}