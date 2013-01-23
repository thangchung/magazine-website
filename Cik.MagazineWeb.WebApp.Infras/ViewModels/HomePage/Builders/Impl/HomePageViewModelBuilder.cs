namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Service.Magazine.Contract;

    public class HomePageViewModelBuilder : ViewModelBuilderBase, IHomePageViewModelBuilder
    {
        private readonly IMagazineService _magazineService;
        private readonly int _numOfPage;

        public HomePageViewModelBuilder(IMagazineService magazineService)
        {
            Guard.ArgumentNotNull(magazineService, "MagazineService");
         
            _magazineService = magazineService;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        public HomePageViewModel Build()
        {
            var cats = _magazineService.GetCategories();

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            headerViewModel.SiteTitle = "Magazine Website";
            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForMainPageLeftColumnViewModel();
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            return mainViewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _magazineService.GetNewestItem(this._numOfPage).ToList();
            mainPageRightCol.MostViews = _magazineService.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();

            var items = _magazineService.GetNewestItem(this._numOfPage);

            if (items != null && items.Any())
            {
                var firstItem = items.First();

                if (firstItem == null)
                    throw new NoNullAllowedException("First Item".ToNotNullErrorMessage());

                if (firstItem.ItemContent == null)
                    throw new NoNullAllowedException("First ItemContent".ToNotNullErrorMessage());

                mainPageLeftCol.FirstItem = firstItem;

                if (items.Count() > 1)
                {
                    mainPageLeftCol.RemainItems = items.Where(x => x.ItemContent != null && x.Id != mainPageLeftCol.FirstItem.Id).ToList();
                }
            }

            return mainPageLeftCol;
        }
    }
}