namespace Cik.MagazineWeb.WebApp.App_Start
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    public class IocConfig
    {
        public static void RegisterIoc(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<WebModule>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); 
        }
    }
}