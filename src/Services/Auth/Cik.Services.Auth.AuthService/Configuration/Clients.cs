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
          ClientName = "api_gateway",
          ClientId = "api_gateway",
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