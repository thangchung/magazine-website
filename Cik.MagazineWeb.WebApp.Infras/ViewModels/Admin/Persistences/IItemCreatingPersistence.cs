namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Persistences
{
    using Cik.MagazineWeb.Model.Magazine;

    public interface IItemCreatingPersistence
    {
        bool PersistenceItem(Item item);
    }
}