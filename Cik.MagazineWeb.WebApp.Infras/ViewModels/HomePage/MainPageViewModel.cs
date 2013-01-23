namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class MainPageViewModel
    {
        public LeftColumnViewModelBase LeftColumn { get; set; }

        public MainPageRightColumnViewModel RightColumn { get; set; }
    }

    public abstract class LeftColumnViewModelBase
    {
        
    }

    public class MainPageLeftColumnViewModel : LeftColumnViewModelBase
    {
        public MainPageLeftColumnViewModel()
        {
            this.RemainItems = new List<ItemDto>();
        }

        public ItemDto FirstItem { get; set; }

        public List<ItemDto> RemainItems { get; set; }
    }

    public class DetailsLeftColumnViewModel : LeftColumnViewModelBase
    {
        public ItemDto CurrentItem { get; set; }
    }

    public class CategoryLeftColumnViewModel : LeftColumnViewModelBase
    {
        public CategoryLeftColumnViewModel()
        {
            this.Items = new List<ItemDto>();    
        }

        public List<ItemDto> Items { get; set; }
    }

    public class MainPageRightColumnViewModel
    {
        public MainPageRightColumnViewModel()
        {
            this.LatestNews = new List<ItemDto>();
            this.MostViews = new List<ItemDto>();
        }

        public List<ItemDto> LatestNews { get; set; }

        public List<ItemDto> MostViews { get; set; }
    }
}