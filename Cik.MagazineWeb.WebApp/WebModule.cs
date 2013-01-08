namespace Cik.MagazineWeb.WebApp
{
    using Autofac;

    using Cik.MagazineWeb.Framework.Configurations;
    using Cik.MagazineWeb.Framework.Encyption.Impl;
    using Cik.MagazineWeb.Repository;
    using Cik.MagazineWeb.Repository.Magazine;
    using Cik.MagazineWeb.Repository.User;
    using Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Builders.Impl;
    using Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin.Persistences.Impl;
    using Cik.MagazineWeb.WebApp.Infras.ViewModels.HomePage.Builders.Impl;

    using Module = Autofac.Module;

    public class WebModule : Module 
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new MainDbContext("DefaultDb")).As<MainDbContext>().SingleInstance();

            builder.RegisterType<CategoryRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().SingleInstance();

            /* home page components */
            builder.RegisterType<HomePageViewModelBuilder>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DetailsViewModelBuilder>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CategoryViewModelBuilder>().AsImplementedInterfaces().SingleInstance();

            /* admin components */
            builder.RegisterType<DashBoardViewModelBuilder>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemCreatingViewModelBuilder>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemEditingViewModelBuilder>().AsImplementedInterfaces().SingleInstance();
    
            builder.RegisterType<ItemCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ExConfigurationManager>().AsImplementedInterfaces().SingleInstance();

            // builder.RegisterType<MediaItemStorage>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<Encryptor>().AsImplementedInterfaces().SingleInstance();
        }
    }
}