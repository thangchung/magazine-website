namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;
    
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            this.Categories = new List<CategoryDto>();
        }

        public string SiteTitle { get; set; }

        public int CurrentCategoryId { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}