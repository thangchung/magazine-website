using System;
using System.Threading.Tasks;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;

namespace Cik.Services.Magazine.MagazineService.CommandHandlers
{
    public class CreateCategoryCommandHandler : IHandleCommand<CreateCategoryCommand>
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CreateCategoryCommandHandler(IRepository<Category, Guid> repo)
        {
            Guard.NotNull(repo);
            _categoryRepository = repo;
        }

        public Task Handle(CreateCategoryCommand message)
        {
            var cat = new Category
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