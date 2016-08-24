using System;
using System.Threading.Tasks;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : IHandleCommand<DeleteCategoryCommand>
    {
        private readonly IRepository<Entities.Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Entities.Category, Guid> repo)
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