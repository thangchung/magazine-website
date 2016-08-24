using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Auth.AuthService.Features.Home
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}