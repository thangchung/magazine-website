namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Builders.Impl
{
    using System.Linq;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Model.Magazine;

    public class ItemCreatingViewModelBuilder : ViewModelBuilderBase, IItemCreatingViewModelBuilder
    {
        private readonly ICategoryRepository _categoryRepository;

        public ItemCreatingViewModelBuilder(ICategoryRepository categoryRepository)
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");

            _categoryRepository = categoryRepository;
        }

        public ItemCreatingViewModel Build()
        {
            var viewModel = new ItemCreatingViewModel();

            viewModel.Categories = this._categoryRepository.GetCategories().ToList();

            return viewModel;
        }
    }
}