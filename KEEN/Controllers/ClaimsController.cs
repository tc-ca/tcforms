using System.Linq;
using System.Security.Claims;
using System.Web.Http;
namespace KEEN.Controllers
{
    [Authorize]
    public class ClaimsController : ApiController
    {
        public dynamic Get()
        {
            var principal = User as ClaimsPrincipal;
            return principal?.Identities.First().Claims.Select(c => new {c.Type, c.Value});
        }
    }
}