using System;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;

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

        public Task Handle(DeleteCategoryCommand message)
        {
            return Task.CompletedTask;
        }
    }
}