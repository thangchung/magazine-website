using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;

namespace Cik.Services.Auth.AuthService.Features.Logout
{
    public class LoggedOutViewModel
    {
        public LoggedOutViewModel()
        {
            SignOutIFrameUrls = Enumerable.Empty<string>();
        }

        public ClientReturnInfo ReturnInfo { get; set; }
        public string ClientName { get; set; }

        public IEnumerable<string> SignOutIFrameUrls { get; set; }
    }
}