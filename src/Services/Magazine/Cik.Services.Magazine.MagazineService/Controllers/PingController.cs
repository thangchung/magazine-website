using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cik.Services.Magazine.MagazineService.Controllers
{
    [Route("ping")]
    [AllowAnonymous]
    public class PingController : Controller
    {
        /* private readonly RestClient _restClient;

        public PingController(RestClient restClient)
        {
            _restClient = restClient;
        } */

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<string>> Get()
        {
            // TODO: just for test the call to another service
            // var result = await _restClient.GetAsync<List<string>>("sample_service", "/api/values");
            // return result;

            return await Task.FromResult(new[] {"pong!!!"});
        }
    }
}