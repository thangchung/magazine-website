namespace Cik.MagazineWeb.Service.Magazine
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;
    using Cik.MagazineWeb.Service.Magazine.Contract;
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class MagazineService : IMagazineService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        public MagazineService(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(itemRepository, "ItemRepository");

            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            return categories.MapTo<CategoryDto>();
        }

        public CategoryDto GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id).MapTo<CategoryDto>();
        }

        public IEnumerable<ItemDto> GetNewestItem(int numOfPage)
        {
            return _itemRepository.GetNewestItem(numOfPage).MapTo<ItemDto>();
        }

        public IEnumerable<ItemDto> GetMostViews(int numOfPage)
        {
            return _itemRepository.GetMostViews(numOfPage).MapTo<ItemDto>();
        }

        public IEnumerable<ItemDto> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords)
        {
            return _itemRepository.SeachByTitle(titleSearchText, index, numOfpage, out numOfRecords).MapTo<ItemDto>();
        }

        public IEnumerable<ItemDto> GetByCategory(int categoryId)
        {
            return _itemRepository.GetByCategory(categoryId).MapTo<ItemDto>();
        }

        public ItemDto GetById(int id)
        {
            return _itemRepository.GetById(id).MapTo<ItemDto>();
        }

        public bool SaveItem(ItemDto item)
        {
            var entity = item.MapTo<Item>();

            return entity != null && this._itemRepository.SaveItem(entity);
        }

        public bool IncreaseNumOfView(int id)
        {
            return _itemRepository.IncreaseNumOfView(id);
        }

        public bool DeleteItem(ItemDto item)
        {
            var entity = item.MapTo<Item>();

            return entity != null && _itemRepository.DeleteItem(entity);
        }
    }
}