namespace Cik.MagazineWeb.WebApp.App_Start
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using global::WebApp;

    public class AppConfig
    {
        public static void Run()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);
            IocConfig.RegisterIoc(GlobalConfiguration.Configuration);
            AutoMapperConfig.RegisterProfiles();
        }
    }
}