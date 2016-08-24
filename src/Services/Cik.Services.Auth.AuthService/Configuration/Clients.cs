using System.Collections.Generic;
using IdentityServer4.Models;

namespace Cik.Services.Auth.AuthService.Configuration
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientName = "magazine_web",
                    ClientId = "magazine_web",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("magazine_web_secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string>
                    {
                        "http://localhost/authorize"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost/unauthorized.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "role",
                        "data_category_records"
                    }
                },
                new Client
                {
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientName = "magazine_web_test",
                    ClientId = "magazine_web_test",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5000/authorize"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5000/unauthorized.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:5000"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "role",
                        "data_category_records"
                    }
                }
            };
        }
    }
}