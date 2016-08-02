using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cik.CoreLibs.Api
{
    public abstract class ControllerBase : Controller
    {
        // TODO: need to add default model into the OkResult
        protected async Task<OkResult> OkResult()
        {
            return await Task.FromResult(Ok());
        }
    }
}