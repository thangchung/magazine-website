namespace Cik.MagazineWeb.Service.Magazine.Contract.Dtos
{
    public class ItemContentDto : DtoBase
    {
        public string Title { get; set; }

        public string SortDescription { get; set; }

        public string Content { get; set; }

        public string SmallImage { get; set; }

        public string MediumImage { get; set; }

        public string BigImage { get; set; }

        public long NumOfView { get; set; }
    }
}