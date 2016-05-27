using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Cik.Data.Abstraction;
using Cik.Services.CategoryService.Model;

namespace Cik.Services.CategoryService.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        public IObservable<Category> GetAll()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Category 1"
                }
            };
            return categories.ToObservable();
        }
    }
}