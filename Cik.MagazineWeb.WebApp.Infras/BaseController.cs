namespace CIK.News.Web.Infras
{
    using System.Security.Authentication;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        protected string GetUserName()
        {
            if (this.User == null || this.User.Identity == null)
            {
                throw new AuthenticationException("You should log in to the system");
            }

            return User.Identity.Name;
        }

        protected void ErrorMessage(string errorMsg)
         {
             this.ViewBag.ErrorMessage = errorMsg;
         }

         protected void SucceedMessage(string succeedMsg)
         {
             this.ViewBag.SucceedMessage = succeedMsg;
         }
    }
}