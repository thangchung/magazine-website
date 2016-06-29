using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Magazine.MagazineService.Controllers
{
  [Route("ping")]
  [AllowAnonymous]
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