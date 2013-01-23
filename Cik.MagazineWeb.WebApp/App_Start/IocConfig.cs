namespace Cik.MagazineWeb.WebApp.App_Start
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Cik.MagazineWeb.Service.Magazine;

    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // register all of autofac modules
            builder.RegisterModule<WebModule>();
            builder.RegisterModule<MagazineModule>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); 
        }
    }
}