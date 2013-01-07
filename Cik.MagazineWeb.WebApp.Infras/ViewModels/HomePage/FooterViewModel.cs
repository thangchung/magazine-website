namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Model.Magazine;

    public class FooterViewModel
    {
        public FooterViewModel()
        {
            this.Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
    }
}