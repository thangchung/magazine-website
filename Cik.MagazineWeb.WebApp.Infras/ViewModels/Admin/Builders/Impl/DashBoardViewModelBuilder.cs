namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Builders.Impl
{
    using System.Configuration;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;

    public class DashBoardViewModelBuilder : ViewModelBuilderBase, IDashBoardViewModelBuilder
    {
        private readonly IItemRepository _itemRepository;

        public DashBoardViewModelBuilder(IItemRepository itemRepository)
        {
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _itemRepository = itemRepository;
        }

        public DashBoardViewModel Build(string titleSearchText, int currentPage)
        {
            var dashBoardViewModel = new DashBoardViewModel();

            int numOfRecords;

            var numOfPage = this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();

            if (numOfPage < 0)
            {
                throw new ConfigurationErrorsException("Cannot find any configuration for PageSize in web.config");
            }

            var items = this._itemRepository.SeachByTitle(titleSearchText, currentPage, numOfPage, out numOfRecords);

            dashBoardViewModel.Items = items.ToList();

            dashBoardViewModel.PagingData = new PagingViewModel(
                                                    currentPage,
                                                    numOfPage,
                                                    numOfRecords,
                                                    string.Format("{0}", "/Admin/DashBoard/Index/{page}"),
                                                     null);
            return dashBoardViewModel;
        }
    }
}