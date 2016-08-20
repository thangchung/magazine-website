using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Cik.CoreLibs.Mvc
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
            yield return "~/Features/{1}/Views/{0}.cshtml";
            yield return "~/Features/SharedViews/{0}.cshtml";
        }
    }
}