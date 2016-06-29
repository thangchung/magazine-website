using System.Collections.Generic;
using IdentityServer4.Models;

namespace Cik.Services.Auth.AuthService.Configuration
{
  public class Scopes
  {
    public static IEnumerable<Scope> Get()
    {
      return new[]
      {
        // standard OpenID Connect scopes
        StandardScopes.OpenId,
        StandardScopes.ProfileAlwaysInclude,
        StandardScopes.EmailAlwaysInclude,

        // API - access token will 
        // contain roles of user
        new Scope
        {
          Name = "data.category.records",
          DisplayName = "Scope for the data category records resource.",
          Type = ScopeType.Resource,
          ScopeSecrets = new List<Secret>
          {
            new Secret("data.category.records.secret".Sha256())
          },
          Claims = new List<ScopeClaim>
          {
            new ScopeClaim("role"),
            new ScopeClaim("data.category.records")
          }
        }
      };
    }
  }
}