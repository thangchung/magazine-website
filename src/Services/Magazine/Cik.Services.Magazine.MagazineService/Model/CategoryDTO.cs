using System;

namespace Cik.Services.Magazine.MagazineService.Model
{
    public class CategoryDto : DtoBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}