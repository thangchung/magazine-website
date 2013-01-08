namespace Cik.MagazineWeb.WebApp.Infras.ActionFilters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using Newtonsoft.Json.Linq;

    public class ValidationActionFilter : ActionFilterAttribute 
    { 
        public override void OnActionExecuting(HttpActionContext context) 
        {
            /* TODO: need for authorize application
            if (HttpContext.Current == null)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized, "1000"); 
            } */

            var modelState = context.ModelState; 
            if (!modelState.IsValid) 
            { 
                var errors = new JObject(); 
                foreach (var key in modelState.Keys) 
                { 
                    var state = modelState[key]; 
                    if (state.Errors.Any()) 
                    { 
                        errors[key] = state.Errors.First().ErrorMessage; 
                    } 
                } 
 
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, errors); 
            } 
        } 
    }
}