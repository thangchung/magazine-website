namespace Cik.MagazineWeb.Model.Magazine
{
    public class ItemFactory
    {
        public static Item CreateItem(string createdBy, Category category, ItemContent itemContent)
        {
            return CreateItem(-1, createdBy, category, itemContent);
        }

        public static Item CreateItem(int id, string createdBy)
        {
            return CreateItem(id, createdBy, new Category(), new ItemContent());
        }

        public static Item CreateItem(int id, string createdBy, Category category, ItemContent itemContent )
        {
            return new Item
                {
                    Id = id,
                    CreatedBy = createdBy,
                    Category = category,
                    ItemContent = itemContent
                };
        }
    }
}