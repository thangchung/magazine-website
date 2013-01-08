namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders
{
    public interface ICategoryViewModelBuilder
    {
        HomePageViewModel Build(int categoryId);
    }
}