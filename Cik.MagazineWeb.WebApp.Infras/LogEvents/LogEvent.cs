namespace Cik.MagazineWeb.WebApp.Infras.LogEvents
{
    using System;
    using System.Web.Management;

    public class LogEvent : WebRequestErrorEvent
    {
        public LogEvent(string message)
            : base(null, null, 100001, new Exception(message))
        {
        }
    }
}