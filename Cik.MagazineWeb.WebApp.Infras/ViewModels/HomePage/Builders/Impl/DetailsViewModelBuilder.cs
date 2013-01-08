namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;

    public class DetailsViewModelBuilder : ViewModelBuilderBase, IDetailsViewModelBuilder
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        private readonly int _numOfPage;

        public DetailsViewModelBuilder(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _numOfPage = ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        public HomePageViewModel Build(int itemId)
        {
            _itemRepository.IncreaseNumOfView(itemId);

            var cats = _categoryRepository.GetCategories();

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

            var item = _itemRepository.GetById(itemId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item id={0}", itemId).ToNotNullErrorMessage());

            viewModel.CurrentItem = item;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = _itemRepository.GetNewestItem(_numOfPage).ToList();
            mainPageRightCol.MostViews = _itemRepository.GetMostViews(_numOfPage).ToList();

            return mainPageRightCol;
        }
    }
}