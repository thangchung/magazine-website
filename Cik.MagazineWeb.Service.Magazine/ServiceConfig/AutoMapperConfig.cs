namespace Cik.MagazineWeb.Service.Magazine.ServiceConfig
{
    using AutoMapper;

    using Cik.MagazineWeb.Service.Magazine.ServiceConfig.Profiles;

    public class AutoMapperConfig
    {
        public static void RegisterProfiles()
        {
            Mapper.AddProfile(new CategoryProfile());
            Mapper.AddProfile(new ItemProfile());
        }
    }
}