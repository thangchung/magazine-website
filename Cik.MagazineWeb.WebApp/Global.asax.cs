namespace Cik.MagazineWeb.WebApp
{
    using Cik.MagazineWeb.WebApp.App_Start;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppConfig.Run();
        }
    }
}