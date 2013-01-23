namespace Cik.MagazineWeb.Service.Magazine.ServiceConfig.Profiles
{
    using AutoMapper;

    using Cik.MagazineWeb.Model.Magazine;
    using Cik.MagazineWeb.Service.Magazine.Contract.Dtos;

    public class ItemProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Item, ItemDto>();
            Mapper.CreateMap<ItemDto, Item>();

            Mapper.CreateMap<ItemContent, ItemContentDto>();
            Mapper.CreateMap<ItemContentDto, ItemContent>();
        }
    }
}