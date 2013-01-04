namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Persistences.Impl
{
    using System;
    using System.Data;

    using Cik.MagazineWeb.Framework.Extensions;
    using Cik.MagazineWeb.Model.Magazine;
    using Cik.MagazineWeb.Model.User;

    public class ItemCreatingPersistence : ViewModelPersistenceBase, IItemCreatingPersistence
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public ItemCreatingPersistence(IItemRepository itemRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            this._itemRepository = itemRepository;
            this._categoryRepository = categoryRepository;
            this._userRepository = userRepository;
        }

        public bool PersistenceItem(Item item)
        {
            var category = this._categoryRepository.GetCategoryById(item.Category.Id);

            if (category == null)
            {
                throw new NoNullAllowedException("Category".ToNotNullErrorMessage());
            }

            item.Category = category;

            item.CreatedDate = DateTime.Now;

            item.ItemContent.CreatedDate = DateTime.Now;

            var user = this._userRepository.GetUserByUserName(item.CreatedBy);

            if (user == null)
            {
                throw new NoNullAllowedException("You have to login to system!!!");
            }

            item.CreatedBy = user.UserName;
            item.ItemContent.CreatedBy = user.UserName;

            return this._itemRepository.SaveItem(item);
        }
    }
}