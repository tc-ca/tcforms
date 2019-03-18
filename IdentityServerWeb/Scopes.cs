using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace IdentityServerWeb
{
    internal static class Scopes
    {
        public static List<Scope> Get()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "keen",
                    DisplayName = "KEEN Forms API",
                    Description = "The API that accesses the form storage and submission system.",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}