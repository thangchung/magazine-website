namespace Cik.CoreLibs.Extensions
{
    public static class ConversionExtensions
    {
        public static int ToInteger(this string str)
        {
            int result;
            if (str == null) return 0;
            if (!int.TryParse(str, out result))
            {
                result = 0;
            }
            return result;
        }

        public static bool ToBoolean(this string str)
        {
            bool result;
            if (str == null) return false;
            if (!bool.TryParse(str, out result))
            {
                result = false;
            }
            return result;
        }
    }
}