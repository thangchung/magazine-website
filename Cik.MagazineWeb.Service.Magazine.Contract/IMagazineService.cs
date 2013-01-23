namespace Cik.MagazineWeb.Service.Magazine.Contract
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public interface IMagazineService
    {
        #region category

        IEnumerable<CategoryDto> GetCategories();

        CategoryDto GetCategoryById(int id);

        #endregion

        #region item

        IEnumerable<ItemDto> GetNewestItem(int numOfPage);

        IEnumerable<ItemDto> GetMostViews(int numOfPage);

        IEnumerable<ItemDto> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords);

        IEnumerable<ItemDto> GetByCategory(int categoryId);

        ItemDto GetById(int id);

        bool SaveItem(ItemDto item);

        bool IncreaseNumOfView(int id);

        bool DeleteItem(ItemDto item);

        #endregion
    }
}