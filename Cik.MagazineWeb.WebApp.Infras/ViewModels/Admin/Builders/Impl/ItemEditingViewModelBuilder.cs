namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Builders.Impl
{
    using System.Data;
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;

    public class ItemEditingViewModelBuilder : ViewModelBuilderBase, IItemEditingViewModelBuilder
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        public ItemEditingViewModelBuilder(
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }

        public ItemEditingViewModel Build(int itemId)
        {
            var item = this._itemRepository.GetById(itemId);

            if (item == null)
            {
                throw new NoNullAllowedException(
                    string.Format("Item with id={0}", itemId).ToNotNullErrorMessage());
            }

            var viewModel = item.MapTo<ItemEditingViewModel>();

            viewModel.Categories = this._categoryRepository.GetCategories().ToList();

            return viewModel;
        }
    }
}