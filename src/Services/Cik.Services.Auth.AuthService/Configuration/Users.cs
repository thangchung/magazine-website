using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Services.InMemory;

namespace Cik.Services.Auth.AuthService.Configuration
{
    internal static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var users = new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "guest",
                    Username = "guest",
                    Password = "123456",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "guest"),
                        new Claim(JwtClaimTypes.GivenName, "guest"),
                        new Claim(JwtClaimTypes.Email, "thangchung@ymail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "guest"),
                        new Claim(JwtClaimTypes.Role, "data_category_records")
                    }
                },
                new InMemoryUser
                {
                    Subject = "admin",
                    Username = "admin",
                    Password = "123456",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "admin"),
                        new Claim(JwtClaimTypes.GivenName, "admin"),
                        new Claim(JwtClaimTypes.Email, "thangchung@ymail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.Role, "data_category_records_admin"),
                        new Claim(JwtClaimTypes.Role, "data_category_records_user"),
                        new Claim(JwtClaimTypes.Role, "data_category_records")
                    }
                },
                new InMemoryUser
                {
                    Subject = "user",
                    Username = "user",
                    Password = "123456",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "user"),
                        new Claim(JwtClaimTypes.GivenName, "user"),
                        new Claim(JwtClaimTypes.Email, "thangchung@ymail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.Role, "data_category_records_user"),
                        new Claim(JwtClaimTypes.Role, "data_category_records")
                    }
                }
            };

            return users;
        }
    }
}