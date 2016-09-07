using System;
using System.Reactive;
using System.Reactive.Linq;
using Cik.CoreLibs.Bus.Amqp;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Events.Handlers
{
    public class CategoryCreatedEventHandler  : AmqpEventHandlerBase<CategoryCreated>
    {
        public CategoryCreatedEventHandler(IUnitOfWork uow) 
            : base(uow)
        {
        }

        public override IObservable<Unit> Handle(CategoryCreated message)
        {
            return Observable.Return(new Unit());
        }
    }
}