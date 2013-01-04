namespace Cik.MagazineWeb.Framework.Extensions
{
    public static class StringExtensions
    {
         public static string GetDefaultPassword(this string source)
         {
             return source + "Hello123";
         }

         public static string GetDefaultEmail(this string source)
         {
             return source + "SiteAdmin@cik.com";
         }

         public static string GetDefaultCreatedByUser(this string source)
         {
             return source + "SiteAdmin";
         }
    }
}