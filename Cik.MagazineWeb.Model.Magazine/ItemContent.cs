namespace Cik.MagazineWeb.Model.Magazine
{
    using System.Collections.Generic;

    public class ItemContent : Entity
    {
        public ItemContent()
        {
            this.Items = new List<Item>();   
        }

        public virtual string Title { get; set; }

        public virtual string SortDescription { get; set; }

        public virtual string Content { get; set; }

        public virtual string SmallImage { get; set; }

        public virtual string MediumImage { get; set; }

        public virtual string BigImage { get; set; }

        public virtual long NumOfView { get; set; }

        public virtual IList<Item> Items { get; set; }
    }
}