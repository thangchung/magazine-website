namespace Cik.MagazineWeb.Model.Magazine
{
    using System.Collections.Generic;

    public interface IItemRepository
    {
        IEnumerable<Item> GetNewestItem(int numOfPage);

        IEnumerable<Item> GetMostViews(int numOfPage);

        IEnumerable<Item> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords);

        IEnumerable<Item> GetByCategory(int categoryId);

        Item GetById(int id);

        bool SaveItem(Item item);

        bool IncreaseNumOfView(int id);

        bool DeleteItem(Item item);
    }
}