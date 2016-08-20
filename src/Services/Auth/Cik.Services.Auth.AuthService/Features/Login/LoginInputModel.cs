using System.ComponentModel.DataAnnotations;

namespace Cik.Services.Auth.AuthService.Features.Login
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }
        public string SignInId { get; set; }
    }
}