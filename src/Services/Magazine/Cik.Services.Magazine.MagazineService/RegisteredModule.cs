using System;
using Autofac;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Model;
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
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
        }
    }
}