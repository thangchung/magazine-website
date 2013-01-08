namespace Cik.MagazineWeb.WebApp.Infras.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    public static class HtmlExtension
    {
         public static MvcHtmlString RenderImageWithPath(this HtmlHelper html, string image, string cssClass, int width, int height)
         {
             var builder = new StringBuilder();

             var rootPath = string.Empty;

             if (HttpContext.Current != null)
             {
                 rootPath += HttpContext.Current.Request.ApplicationPath;
             }

             builder.AppendFormat(
                 "<img src=\"{5}/App_Data/Uploads/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>", 
                    image, 
                    image, 
                    cssClass, 
                    width,
                    height,
                    rootPath);

             return new MvcHtmlString(builder.ToString());
         }


         public static MvcHtmlString RenderImageWithPath<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TModel model, Expression<Func<TModel, TProperty>> expression, string cssClass, int width, int height)
         {
             var func = expression.Compile();

             var value = func.Invoke(model);

             var builder = new StringBuilder();

             var rootPath = string.Empty;

             if (HttpContext.Current != null)
             {
                 rootPath += HttpContext.Current.Request.ApplicationPath;
             }

             builder.AppendFormat(
                 "<img src=\"{5}/App_Data/Uploads/{0}\" alt=\"{1}\" class=\"{2}\" width=\"{3}\" height=\"{4}\" //>",
                    value,
                    value,
                    cssClass,
                    width,
                    height,
                    rootPath);

             return new MvcHtmlString(builder.ToString());
         }
    }
}