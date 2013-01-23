namespace Cik.MagazineWeb.Service
{
    using System;

    public abstract class DtoBase
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}