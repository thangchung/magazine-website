using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Cik.Services.Auth.AuthService.UI
{
  public class CustomViewLocationExpander : IViewLocationExpander
  {
    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }

    public IEnumerable<string> ExpandViewLocations(
      ViewLocationExpanderContext context,
      IEnumerable<string> viewLocations)
    {
      yield return "~/UI/{1}/Views/{0}.cshtml";
      yield return "~/UI/SharedViews/{0}.cshtml";
    }
  }
}