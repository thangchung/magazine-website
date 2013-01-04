namespace Cik.MagazineWeb.WebApp.Infras.ViewModels
{
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Configurations;

    public abstract class ViewModelBuilderBase
    {
        protected readonly IExConfigurationManager ConfigurationManager;

        protected ViewModelBuilderBase()
            : this(DependencyResolver.Current.GetService<IExConfigurationManager>())
        {
        }

        protected ViewModelBuilderBase(IExConfigurationManager configurationManager)
        {
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            ConfigurationManager = configurationManager;
        }
    }
}