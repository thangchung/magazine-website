namespace Cik.MagazineWeb.Repository.Magazine
{
    using System.Collections.Generic;
    using System.Linq;

    using Cik.MagazineWeb.Data;
    using Cik.MagazineWeb.Model.Magazine;

    public class CategoryRepository : GenericRepository, ICategoryRepository
    {
        public CategoryRepository(MainDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.GetQuery<Category>().ToList();
        }

        public Category GetCategoryById(int id)
        {
            return this.FindOne<Category>(x => x.Id == id);
        }
    }
}