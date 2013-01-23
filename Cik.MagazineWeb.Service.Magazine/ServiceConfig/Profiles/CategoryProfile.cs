namespace Cik.MagazineWeb.Service.Magazine.ServiceConfig.Profiles
{
    using AutoMapper;

    using Cik.MagazineWeb.Model.Magazine;
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();
        }
    }
}