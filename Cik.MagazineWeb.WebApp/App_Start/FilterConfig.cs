using System.Web;
using System.Web.Mvc;

namespace WebApp
{
    using Cik.MagazineWeb.WebApp.Infras.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}