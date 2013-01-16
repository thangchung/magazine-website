namespace Cik.MagazineWeb.Model.Magazine
{
    using System.Collections.Generic;

    public class Category : Entity
    {
        public Category()
        {
            this.Items = new List<Item>();
        }

        public virtual string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
