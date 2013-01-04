namespace Cik.MagazineWeb.Model.Magazine
{
    using System.Collections.Generic;

    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);
    }
}