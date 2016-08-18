using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cik.CoreLibs.Filters
{
    public class ValidationExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.Result = new JsonResult(context.Exception.Message);
            }
        }
    }
}