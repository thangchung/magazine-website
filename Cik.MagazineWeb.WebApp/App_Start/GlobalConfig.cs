namespace Cik.MagazineWeb.WebApp.App_Start
{
    using System.Web.Helpers;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Cik.MagazineWeb.WebApp.Infras.ActionFilters;
    using Cik.MagazineWeb.WebApp.Infras.Filters;
    using Cik.MagazineWeb.WebApp.Infras.WebApi.Invokers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class GlobalConfig
    {
        public static void CustomizeConfig(HttpConfiguration config)
        {
            // Remove Xml formatters. This means when we visit an endpoint from a browser,
            // Instead of returning Xml, it will return Json.
            // More information from Dave Ward: http://jpapa.me/P4vdx6
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.Insert(0, new JsonpMediaTypeFormatter());

            // Configure json camelCasing per the following post: http://jpapa.me/NqC2HH
            // Here we configure it to write JSON property names with camel casing
            // without changing our server-side data model:
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Add model validation, globally
            config.Filters.Add(new ValidationActionFilter());

            // config for always show details error message 
            // http://lostechies.com/jimmybogard/2012/04/18/custom-errors-and-error-detail-policy-in-asp-net-web-api/
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // register cors handler http://patelshailesh.com/index.php/cross-domain-asp-net-web-api-call-using-ajax
            // GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsHandler());

            config.Filters.Add(new ExceptionHandlingAttribute());
        }
    }
}