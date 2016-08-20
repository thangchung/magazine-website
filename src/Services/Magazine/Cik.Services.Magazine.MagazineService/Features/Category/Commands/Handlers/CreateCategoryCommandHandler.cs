using System;
using System.Threading.Tasks;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Features.Category.Commands.Handlers
{
    public class CreateCategoryCommandHandler : IHandleCommand<CreateCategoryCommand>
    {
        private readonly IRepository<Entity.Category, Guid> _categoryRepository;

        public CreateCategoryCommandHandler(IRepository<Entity.Category, Guid> repo)
        {
            Guard.NotNull(repo);
            _categoryRepository = repo;
        }

        public Task Handle(CreateCategoryCommand message)
        {
            var cat = new Entity.Category
            {
                Id = Guid.NewGuid(),
                Name = message.Name
            };
            _categoryRepository.Create(cat).Subscribe(x => { });
            _categoryRepository.UnitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}