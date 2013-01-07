namespace Cik.MagazineWeb.WebApp.Controllers
{
    using System.Web.Mvc;

    using CIK.News.Web.Infras;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders;

    public class HomeController : BaseController
    {
        private readonly IHomePageViewModelBuilder _homePageVMBuilder;

        public HomeController(IHomePageViewModelBuilder homePageVmBuilder)
        {
            Guard.ArgumentNotNull(homePageVmBuilder, "HomePageVmBuilder");

            _homePageVMBuilder = homePageVmBuilder;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homePageViewModel = _homePageVMBuilder.Build();

            return this.View(homePageViewModel);
        }
    }
}
