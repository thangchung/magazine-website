namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Service.Magazine.Contract;

    public class CategoryViewModelBuilder : ViewModelBuilderBase, ICategoryViewModelBuilder
    {
        private readonly int _numOfPage;
        
        private readonly IMagazineService _magazineService;

        public CategoryViewModelBuilder(IMagazineService magazineService)
        {
            Guard.ArgumentNotNull(magazineService, "MagazineService");
         
            _magazineService = magazineService;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        public HomePageViewModel Build(int categoryId)
        {
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

            mainPageViewModel.LeftColumn = BindingDataForCategoryLeftColumnViewModel(categoryId);
            mainPageViewModel.RightColumn = BindingDataForMainPageRightColumnViewModel();

            var items = ((CategoryLeftColumnViewModel)mainPageViewModel.LeftColumn).Items;
            if (items != null && items.Count > 0)
                headerViewModel.SiteTitle = string.Format("Magazine Website - {0} category", items.FirstOrDefault().Category.Name);
            else
                headerViewModel.SiteTitle = "Magazine Website";

            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            return mainViewModel;
        }

        private CategoryLeftColumnViewModel BindingDataForCategoryLeftColumnViewModel(int categoryId)
        {
            var viewModel = new CategoryLeftColumnViewModel();

            var items = _magazineService.GetByCategory(categoryId);

            if (items == null)
                throw new NoNullAllowedException("Items".ToNotNullErrorMessage());

            if (items.Any())
            {
                viewModel.Items = items.ToList();
            }

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