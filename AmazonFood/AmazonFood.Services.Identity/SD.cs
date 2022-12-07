using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static System.Net.WebRequestMethods;

namespace AmazonFood.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("amazonfood","Amazon Server")
                //new ApiScope(name:"read",displayName:"Read your data."),
                //new ApiScope(name:"write",displayName:"Read your data."),
                //new ApiScope(name:"delete",displayName:"Read your data.")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes={"read","write","profile"}
                },
                 new Client
                {
                    ClientId="amazonfood",
                    ClientSecrets={new Secret("secret".Sha256())},
                     AllowedGrantTypes=GrantTypes.Code,
                    
                    RedirectUris={ "https://localhost:7072/signin-oidc" },
                    PostLogoutRedirectUris={"https://localhost:7072/singout-callback-oidc"},
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "AmazonFood"
                    }
                },
            };
    }
}
