namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Builders
{
    public interface IDashBoardViewModelBuilder
    {
        DashBoardViewModel Build(string titleSearchText, int currentPage);
    }
}