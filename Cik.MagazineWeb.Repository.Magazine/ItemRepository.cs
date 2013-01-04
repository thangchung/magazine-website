namespace Cik.MagazineWeb.Repository.Magazine
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Cik.MagazineWeb.Data;
    using Cik.MagazineWeb.Model.Magazine;

    public class ItemRepository : GenericRepository, IItemRepository
    {
        public ItemRepository(MainDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Item> GetNewestItem(int numOfPage)
        {
            return this.GetItems().OrderByDescending(x => x.CreatedDate).Take(numOfPage);
        }

        public IEnumerable<Item> GetMostViews(int numOfPage)
        {
            return this.GetItems().OrderByDescending(x => x.ItemContent.NumOfView).Take(numOfPage);
        }

        public IEnumerable<Item> SeachByTitle(string titleSearchText, int index, int numOfpage, out int numOfRecords)
        {
            var items = this.GetItems();

            numOfRecords = items.Count();

            items = items.Where(x => x.ItemContent.Title.Contains(titleSearchText));

            return items.Skip((index - 1) * numOfpage).Take(numOfpage);
        }

        public IEnumerable<Item> GetByCategory(int categoryId)
        {
            var items = this.GetItems();

            return items.Where(x => x.Category.Id == categoryId);
        }

        public Item GetById(int id)
        {
            return this.GetItems().First(x => x.Id == id);
        }

        public bool SaveItem(Item item)
        {
            if (item.Id > 0)
            {
                this.Update(item);
            }
            else
            {
                this.Add(item);
            }

            this.UnitOfWork.SaveChanges();

            return true;
        }

        public bool IncreaseNumOfView(int id)
        {
            var item = this.GetById(id);

            if (item != null)
            {
                item.ItemContent.NumOfView = item.ItemContent.NumOfView + 1;

                return this.SaveItem(item);
            }

            return false;
        }

        public bool DeleteItem(Item item)
        {
            this.Delete(item);

            this.UnitOfWork.SaveChanges();

            return true;
        }

        private IEnumerable<Item> GetItems()
        {
            var items = this.GetQuery<Item>()
                            .Include(x => x.ItemContent)
                            .ToList();

            // TODO: will remove query here, i dont know why include ItemContent didn't work
            foreach (var item in items)
            {
                item.ItemContent = this.GetQuery<ItemContent>().First(x => x.Id == item.ItemContentId);
            }

            return items;
        }
    }
}