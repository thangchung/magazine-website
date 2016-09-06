using System;
using System.Reactive;
using Cik.CoreLibs.Bus.Amqp;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : AmqpCommandHandlerBase<DeleteCategoryCommand>
    {
        private readonly IRepository<Entities.Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(
            IUnitOfWork uow,
            IRepository<Entities.Category, Guid> repo)
            : base(uow)
        {
            _categoryRepository = repo;
        }

        public override IObservable<Unit> Handle(DeleteCategoryCommand message)
        {
            throw new NotImplementedException();
        }
    }
}