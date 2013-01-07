namespace Cik.MagazineWeb.WebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Cik.MagazineWeb.Model.Magazine;

    using CodeCamper.Web.Controllers;

    // [Authorize]
    public class DashboardController : ApiControllerBase
    {
        public IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>();
            categories.Add(new Category
                {
                    Id = 1,
                    Name = "abc",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "T.Chung"
                });

            categories.Add(new Category
            {
                Id = 2,
                Name = "def",
                CreatedDate = DateTime.Now,
                CreatedBy = "T.Chung"
            });

            if (categories.Count >= 0)
            {
                return categories;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}