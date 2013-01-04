namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Model.Magazine;

    public class DashBoardViewModel
    {
        public DashBoardViewModel()
        {
            this.Items = new List<Item>();
            this.PagingData = new PagingViewModel();
        }

        public string TitleSearchText { get; set; }

        public List<Item> Items { get; set; }

        public PagingViewModel PagingData { get; set; }
    }
}