namespace Cik.MagazineWeb.WebApp.Controllers
{
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders;

    using CIK.News.Web.Infras;

    public class HomeController : BaseController
    {
        private readonly IHomePageViewModelBuilder _homePageVMBuilder;

        private readonly IDetailsViewModelBuilder _detailsVMBuilder;

        private readonly ICategoryViewModelBuilder _categoryVMBuilder;

        public HomeController(IHomePageViewModelBuilder homePageVmBuilder, IDetailsViewModelBuilder detailsVmBuilder, ICategoryViewModelBuilder categoryVmBuilder)
        {
            Guard.ArgumentNotNull(homePageVmBuilder, "HomePageVmBuilder");
            Guard.ArgumentNotNull(detailsVmBuilder, "DetailsViewModelBuilder");
            Guard.ArgumentNotNull(categoryVmBuilder, "CategoryViewModelBuilder");

            _homePageVMBuilder = homePageVmBuilder;
            _detailsVMBuilder = detailsVmBuilder;
            _categoryVMBuilder = categoryVmBuilder;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homePageViewModel = _homePageVMBuilder.Build();

            return View(homePageViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var homePageViewModel = _detailsVMBuilder.Build(id);

            return View(homePageViewModel);
        }

        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            var homePageViewModel = _categoryVMBuilder.Build(id);

            return View(homePageViewModel);
        }
    }
}
