using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace KEEN.IDSrv
{
    internal static class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                // no human involved
                new Client
                {
                    ClientName = "TCFORMS",
                    ClientId = Config.ClientId,

                    //TODO: Convert to JWT as per GCAPI -- Proper SSL signing required
                    AccessTokenType = AccessTokenType.Reference,

                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        // sample z-base-32 random
                        new Secret(Config.TcFormsSecret.Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "keen"
                    }
                }
            };
        }
    }
}
