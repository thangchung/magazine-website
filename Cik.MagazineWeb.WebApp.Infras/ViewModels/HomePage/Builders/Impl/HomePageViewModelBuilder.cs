namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;

    public class HomePageViewModelBuilder : ViewModelBuilderBase, IHomePageViewModelBuilder
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _numOfPage;

        public HomePageViewModelBuilder(
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        public HomePageViewModel Build()
        {
            var cats = this._categoryRepository.GetCategories();

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

            mainPageRightCol.LatestNews = this._itemRepository.GetNewestItem(this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();

            var items = this._itemRepository.GetNewestItem(this._numOfPage);

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