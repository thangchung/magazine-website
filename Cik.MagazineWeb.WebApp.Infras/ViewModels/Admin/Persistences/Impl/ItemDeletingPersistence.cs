namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Persistences.Impl
{
    using System.Data;

    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;

    public class ItemDeletingPersistence : ViewModelPersistenceBase, IItemDeletingPersistence
    {
        private readonly IItemRepository _itemRepository;

        public ItemDeletingPersistence(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        public bool PersistenceItem(int id)
        {
            var item = this._itemRepository.GetById(id);

            if (item == null)
            {
                throw new NoNullAllowedException(string.Format("Item with id={0}", id).ToNotNullErrorMessage());
            }

            return this._itemRepository.DeleteItem(item);
        }
    }
}