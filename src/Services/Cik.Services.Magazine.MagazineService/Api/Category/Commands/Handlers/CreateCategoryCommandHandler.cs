using System;
using System.Reactive;
using System.Reactive.Linq;
using Cik.CoreLibs;
using Cik.CoreLibs.Bus.Amqp;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands.Handlers
{
    public class CreateCategoryCommandHandler : AmqpCommandHandlerBase<CreateCategoryCommand>
    {
        private readonly IRepository<Entities.Category, Guid> _categoryRepository;

        public CreateCategoryCommandHandler(
            IUnitOfWork uow,
            IRepository<Entities.Category, Guid> repo
            ) : base(uow)
        {
            Guard.NotNull(repo);
            _categoryRepository = repo;
        }

        public override IObservable<Unit> Handle(CreateCategoryCommand message)
        {
            var retStream = _categoryRepository
                .Create(
                    new Entities.Category
                    {
                        Id = Guid.NewGuid(),
                        Name = message.Name
                    })
                .Select(x => new Unit());
            UnitOfWork.SaveChanges();
            return retStream;
        }
    }
}