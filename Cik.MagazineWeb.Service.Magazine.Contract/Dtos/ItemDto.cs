namespace Cik.MagazineWeb.Service.Magazine.Contract.Dtos
{
    public class ItemDto : DtoBase
    {
        public int ItemContentId { get; set; }

        public CategoryDto Category { get; set; }

        public ItemContentDto ItemContent { get; set; }
    }
}