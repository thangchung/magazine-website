namespace Cik.MagazineWeb.Framework.Extensions
{
    using System.IO;

    public static class MemoryStreamExtension
    {
         public static void WriteTo(this MemoryStream memoryStream, string fileName)
         {
             var outStream = File.OpenWrite(fileName);
             memoryStream.WriteTo(outStream);
             outStream.Flush();
             outStream.Close();
         }
    }
}