using System;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Shared.Domain;

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

        public void Handle(CreateCategoryCommand message)
        {
            var cat = new Category();
            cat.Id = Guid.NewGuid();
            cat.Name = message.Name;
            _categoryRepository.Create(cat);
            _categoryRepository.UnitOfWork.SaveChanges();
        }
    }
}