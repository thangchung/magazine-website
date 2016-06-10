using System;
using Autofac;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Command;
using Cik.Services.Magazine.MagazineService.CommandHandlers;
using Cik.Services.Magazine.MagazineService.Extensions;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.QueryModel;
using Cik.Services.Magazine.MagazineService.Repository;
using Cik.Services.Magazine.MagazineService.Service;

namespace Cik.Services.Magazine.MagazineService
{
    public class RegisteredModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MagazineDbContext>().AsSelf();

            // register command model
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterCommandHandlerWith(typeof (CreateCategoryCommand));
            builder.Register(ctx =>
            {
                var bus = new InMemoryBus();
                bus.RegisterHandler<CreateCategoryCommand>(ctx.Resolve<IHandleCommand<CreateCategoryCommand>>().Handle);
                return bus;
            }).As<ICommandHandler>().SingleInstance();

            // register query model
            builder.RegisterType<CategoryQueryModelFinder>().AsSelf();
        }
    }
}