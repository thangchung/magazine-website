namespace Cik.MagazineWeb.WebApp.Infras.WebApi.Invokers
{
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    public class CikApiControllerActionInvoker : ApiControllerActionInvoker
    {
        public override Task<HttpResponseMessage> InvokeActionAsync(
            HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            var result = base.InvokeActionAsync(actionContext, cancellationToken);

            if (result.Exception != null && result.Exception.GetBaseException() != null)
            {
                var baseException = result.Exception.GetBaseException();

                //if (baseException is BusinessException)
                //{
                //    return Task.Run<HttpResponseMessage>(() => new HttpResponseMessage(HttpStatusCode.InternalServerError)
                //                                                {
                //                                                    Content = new StringContent(baseException.Message),
                //                                                    ReasonPhrase = "Error"

                //                                                });
                //}
                //else
                //{
                // Log critical error
                Debug.WriteLine(baseException);

                return Task.Run(
                    () => new HttpResponseMessage(HttpStatusCode.InternalServerError)
                        {
                                      Content = new StringContent(baseException.Message),
                                      ReasonPhrase = "Critical Error"
                                  });
                // }
            }

            return result;
        }
    }
}