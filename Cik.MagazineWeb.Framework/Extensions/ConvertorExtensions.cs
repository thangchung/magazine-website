namespace Cik.MagazineWeb.Framework.Extensions
{
    public static class ConvertorExtensions
    {
         public static int ToInteger(this string source)
         {
             int result;

             int.TryParse(source, out result);

             return result;
         }
    }
}