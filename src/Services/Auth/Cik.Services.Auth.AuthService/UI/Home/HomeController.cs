using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Auth.AuthService.UI.Home
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