namespace CodeCamper.Web.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Configurations;

    public abstract class ApiControllerBase : ApiController
    {
        protected readonly IExConfigurationManager ConfigurationManager;

        protected ApiControllerBase()
            : this(DependencyResolver.Current.GetService<IExConfigurationManager>())
        {
        }

        protected ApiControllerBase(IExConfigurationManager configurationManager)
        {
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            ConfigurationManager = configurationManager;
        }
    }
}
