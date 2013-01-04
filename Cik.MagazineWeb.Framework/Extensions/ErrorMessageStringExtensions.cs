namespace Cik.MagazineWeb.Framework.Extensions
{
    using Cik.MagazineWeb.Framework.Contants;

    public static class ErrorMessageStringExtensions
    {
         public static string ToNotNullErrorMessage(this string source)
         {
             return string.Format(ConstantMessage.ShouldNotBeNull, source);
         }
    }
}