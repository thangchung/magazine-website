namespace Cik.MagazineWeb.WebApp.Controllers.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Model.Magazine;

    using CodeCamper.Web.Controllers;

    // [Authorize]
    public class DashboardController : ApiControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public DashboardController()
            : this(DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public DashboardController(ICategoryRepository categoryRepository)
        {
            Guard.ArgumentNotNull(categoryRepository, "CategoryRepository");

            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategories()
        {
            //var categories = new List<Category>();
            //categories.Add(new Category
            //    {
            //        Id = 1,
            //        Name = "abc",
            //        CreatedDate = DateTime.Now,
            //        CreatedBy = "T.Chung"
            //    });

            //categories.Add(new Category
            //{
            //    Id = 2,
            //    Name = "def",
            //    CreatedDate = DateTime.Now,
            //    CreatedBy = "T.Chung"
            //});

            var categories = _categoryRepository.GetCategories();

            if (categories != null && categories.Count() >= 0)
            {
                return categories;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}