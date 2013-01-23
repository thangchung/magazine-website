namespace Cik.MagazineWeb.WebApp.Controllers.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Mvc;

    using Cik.MagazineWeb.Framework;
    using Cik.MagazineWeb.Service.Magazine.Contract;
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    using CodeCamper.Web.Controllers;

    public class DashboardController : ApiControllerBase
    {
        private readonly IMagazineService _magazineService;

        public DashboardController()
            : this(DependencyResolver.Current.GetService<IMagazineService>())
        {
        }

        public DashboardController(IMagazineService magazineService)
        {
            Guard.ArgumentNotNull(magazineService, "MagazineService");

            _magazineService = magazineService;
        }

        // http://www.strathweb.com/2012/03/serializing-entity-framework-objects-to-json-in-asp-net-web-api/
        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _magazineService.GetCategories();

            if (categories != null && categories.Count() >= 0)
            {
                return categories;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}