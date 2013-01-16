namespace Cik.MagazineWeb.WebApp.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Model.Magazine;

    using CodeCamper.Web.Controllers;

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

        public dynamic GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            if (categories != null && categories.Count() >= 0)
            {
                return categories;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}