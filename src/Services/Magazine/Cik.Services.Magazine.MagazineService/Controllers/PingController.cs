using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Magazine.MagazineService.Controllers
{
    [Route("ping")]
    public class PingController : Controller
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new[] {"pong!!!"};
        }
    }
}