using System;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Shared.Domain;

namespace Cik.Services.Magazine.MagazineService.CommandHandlers
{
    public class DeleteCategoryCommandHandler : IHandleCommand<DeleteCategoryCommand>
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Category, Guid> repo)
        {
            Guard.NotNull(repo);

            _categoryRepository = repo;
        }

        public void Handle(DeleteCategoryCommand message)
        {
        }
    }
}