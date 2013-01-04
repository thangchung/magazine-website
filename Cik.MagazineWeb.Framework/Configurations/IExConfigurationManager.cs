namespace Cik.MagazineWeb.Framework.Configurations
{
    using System.Collections.Specialized;
    using System.Configuration;

    public interface IExConfigurationManager
    {
        object GetSection(string sectionName);

        ConnectionStringSettingsCollection GetConnectionStrings();

        NameValueCollection GetAppSettings();

        string GetAppConfigBy(string appConfigName); 
    }
}