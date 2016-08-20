using System;
using System.Threading.Tasks;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Features.Category.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : IHandleCommand<DeleteCategoryCommand>
    {
        private readonly IRepository<Entity.Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Entity.Category, Guid> repo)
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