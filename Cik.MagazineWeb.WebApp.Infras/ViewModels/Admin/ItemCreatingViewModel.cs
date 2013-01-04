namespace Cik.MagazineWeb.WebApp.Infras.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ItemCreatingViewModel : ItemViewModel
    {
        [Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase SmallImage { get; set; }

        [Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase MediumImage { get; set; }

        [Required, FileExtensions(Extensions = "jpg", ErrorMessage = "Specify a jpg file. (Comma-separated values)")]
        public HttpPostedFileBase BigImage { get; set; } 
    }
}