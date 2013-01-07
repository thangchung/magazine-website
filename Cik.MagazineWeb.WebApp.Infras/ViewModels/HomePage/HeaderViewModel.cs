namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Model.Magazine;

    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            this.Categories = new List<Category>();
        }

        public string SiteTitle { get; set; }

        public int CurrentCategoryId { get; set; }

        public List<Category> Categories { get; set; }
    }
}