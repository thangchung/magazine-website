using System;
using Autofac;
using Cik.Data.Abstraction;
using Cik.Services.CategoryService.Model;
using Cik.Services.CategoryService.Repository;
using Cik.Services.CategoryService.Service;

namespace Cik.Services.CategoryService
{
    public class RegisteredModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<CategoryDbContext>().AsSelf();
            builder.RegisterType<CategoryRepository>().As<IRepository<Category, Guid>>();
            builder.RegisterType<Service.CategoryService>().As<ICategoryService>();
        }
    }
}