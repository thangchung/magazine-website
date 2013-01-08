namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders
{
    public interface IDetailsViewModelBuilder
    {
        HomePageViewModel Build(int itemId);
    }
}