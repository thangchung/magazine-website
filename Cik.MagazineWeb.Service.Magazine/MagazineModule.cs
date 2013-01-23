namespace Cik.MagazineWeb.Service.Magazine
{
    using Autofac;

    using Cik.MagazineWeb.Service.Magazine.ServiceConfig;

    public class MagazineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MagazineService>().AsImplementedInterfaces().SingleInstance();

            AutoMapperConfig.RegisterProfiles();
        }
    }
}