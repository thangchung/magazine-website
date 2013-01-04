namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Persistences
{
    public interface IItemDeletingPersistence
    {
        bool PersistenceItem(int id);
    }
}