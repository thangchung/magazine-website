namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage
{
    using System.Collections.Generic;

    using Cik.MagazineWeb.Model.Magazine;

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
            this.RemainItems = new List<Item>();
        }

        public Item FirstItem { get; set; }

        public List<Item> RemainItems { get; set; }
    }

    public class DetailsLeftColumnViewModel : LeftColumnViewModelBase
    {
        public Item CurrentItem { get; set; }
    }

    public class CategoryLeftColumnViewModel : LeftColumnViewModelBase
    {
        public CategoryLeftColumnViewModel()
        {
            this.Items = new List<Item>();    
        }

        public List<Item> Items { get; set; }
    }

    public class MainPageRightColumnViewModel
    {
        public MainPageRightColumnViewModel()
        {
            this.LatestNews = new List<Item>();
            this.MostViews = new List<Item>();
        }

        public List<Item> LatestNews { get; set; }

        public List<Item> MostViews { get; set; }
    }
}