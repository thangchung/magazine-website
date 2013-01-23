namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Service.Magazine.Contract;

    public class DetailsViewModelBuilder : ViewModelBuilderBase, IDetailsViewModelBuilder
    {
        private readonly IMagazineService _magazineService;

        private readonly int _numOfPage;

        public DetailsViewModelBuilder(IMagazineService magazineService)
        {
            Guard.ArgumentNotNull(magazineService, "MagazineService");
         
            _magazineService = magazineService;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        public HomePageViewModel Build(int itemId)
        {
            _magazineService.IncreaseNumOfView(itemId);

            var cats = _magazineService.GetCategories();

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = BindingDataForDetailsLeftColumnViewModel(itemId);
            mainPageViewModel.RightColumn = BindingDataForMainPageRightColumnViewModel();

            headerViewModel.SiteTitle = string.Format("Magazine Website - {0}",
                ((DetailsLeftColumnViewModel)mainPageViewModel.LeftColumn).CurrentItem.ItemContent.Title);

            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            return mainViewModel;
        }

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int itemId)
        {
            var viewModel = new DetailsLeftColumnViewModel();

            var item = _magazineService.GetById(itemId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item id={0}", itemId).ToNotNullErrorMessage());

            viewModel.CurrentItem = item;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _magazineService.GetNewestItem(_numOfPage).ToList();
            mainPageRightCol.MostViews = _magazineService.GetMostViews(_numOfPage).ToList();

            return mainPageRightCol;
        }
    }
}