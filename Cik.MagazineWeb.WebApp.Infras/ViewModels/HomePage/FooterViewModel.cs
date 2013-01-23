namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;
    
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class FooterViewModel
    {
        public FooterViewModel()
        {
            this.Categories = new List<CategoryDto>();
        }

        public List<CategoryDto> Categories { get; set; }
    }
}