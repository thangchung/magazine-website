using System;
using Autofac;
using Cik.Data.Abstraction;
using Cik.Services.MagazineService.Model;
using Cik.Services.MagazineService.Repository;
using Cik.Services.MagazineService.Service;

namespace Cik.Services.MagazineService
{
    public class RegisteredModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MagazineDbContext>().AsSelf();
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>();
            builder.RegisterType<Service.CategoryService>().As<ICategoryService>();
        }
    }
}