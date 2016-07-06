using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cik.Rest;
using Cik.Services.Magazine.MagazineService.Model;

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
      // var client = new RestClient();
      // var aa = client.Get<CategoryDto>("http://localhost:5000", "/api/categories").Result;

      return new[] {"pong!!!"};
    }
  }
}