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
          ClientName = "api_gateway",
          ClientId = "api_gateway",
          AllowedGrantTypes = GrantTypes.Implicit,
          AllowAccessTokensViaBrowser = true,
          RedirectUris = new List<string>
          {
            "https://localhost:8888/authorized"
          },
          PostLogoutRedirectUris = new List<string>
          {
            "https://localhost:8888/unauthorized.html"
          },
          AllowedCorsOrigins = new List<string>
          {
            "https://localhost:8888"
          },
          AllowedScopes = new List<string>
          {
            "openid",
            "email",
            "profile",
            "role",
            "data.category.records"
          }
        }
      };
    }
  }
}