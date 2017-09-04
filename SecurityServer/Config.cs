using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace SecurityServer
{
    public class Config
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("resourcesScope", "My API") { UserClaims = { "role" } } };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="PostmanClient",
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                   AllowAccessTokensViaBrowser=true,
                   AllowOfflineAccess=true,
                   AlwaysSendClientClaims=true,
                   RequireClientSecret=false,
                   AllowedScopes =
                    {
                      IdentityServerConstants.StandardScopes.Profile,
                      "roles",
                      "resourceScope"
                    }
                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            throw new NotImplementedException();
        }
    }
}
