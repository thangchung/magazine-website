namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ItemEditingViewModel : ItemViewModel
    {
        public int ItemId { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase SmallImage { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase MediumImage { get; set; }

        [FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase BigImage { get; set; } 

        public string SmallImagePath { get; set; }

        public string MediumImagePath { get; set; }

        public string BigImagePath { get; set; }
    }
}