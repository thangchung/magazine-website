namespace Cik.MagazineWeb.Model.Magazine
{
    public class Item : Entity
    {
        public Item()
        {
            this.Category = new Category();
            this.ItemContent = new ItemContent();
        }

        public virtual int ItemContentId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ItemContent ItemContent { get; set; }
    }
}