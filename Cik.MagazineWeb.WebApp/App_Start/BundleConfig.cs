namespace WebApp
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;

            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*intellisense.js");

            // Modernizr goes separate since it loads first
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/libs/modernizr-{version}.js"));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                "//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js")
                .Include("~/Scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytmpl",
                "//raw.github.com/jquery/jquery-tmpl/master/jquery.tmpl.js")
                .Include("~/Scripts/libs/jquery.tmpl.js"));

            // 3rd Party JavaScript files
            bundles.Add(new ScriptBundle("~/bundles/jsextlibs")
                .Include(
                    "~/Scripts/libs/json2.js", // IE7 needs this

                    // jQuery plugins
                    "~/Scripts/libs/activity-indicator.js",
                    "~/Scripts/libs/jquery.mockjson.js",
                    "~/Scripts/libs/TrafficCop.js",
                    "~/Scripts/libs/infuser.js", // depends on TrafficCop

                    // Knockout and its plugins
                    "~/Scripts/libs/knockout-{version}.js",
                    "~/Scripts/libs/knockout.activity.js",
                    "~/Scripts/libs/knockout.asyncCommand.js",
                    "~/Scripts/libs/knockout.dirtyFlag.js",
                    "~/Scripts/libs/knockout.validation.js",
                    "~/Scripts/libs/koExternalTemplateEngine.js",
                    "~/Scripts/libs/knockout.simpleGrid.1.3.js",

                    // Other 3rd party libraries
                    "~/Scripts/libs/underscore.js",
                    "~/Scripts/libs/moment.js",
                    "~/Scripts/libs/sammy.*",
                    "~/Scripts/libs/amplify.*",
                    "~/Scripts/libs/toastr.js"
                    ));

            //bundles.Add(new ScriptBundle("~/bundles/jsmocks")
            //    .IncludeDirectory("~/Scripts/apps/mock", "*.js", searchSubdirectories: false));

            //// All application JS files (except mocks)
            //bundles.Add(new ScriptBundle("~/bundles/jsapplibs")
            //    .IncludeDirectory("~/Scripts/apps/", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle("~/bundles/jsmocks")
                .IncludeDirectory("~/Scripts/myapps/mocks", "*.js", searchSubdirectories: false));

            // All application JS files (except mocks)
            bundles.Add(new ScriptBundle("~/bundles/jsapplibs")
                .IncludeDirectory("~/Scripts/myapps/", "*.js", searchSubdirectories: true));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/normalize.css",
                "~/Content/main.css",
                "~/Content/style.css"));
        }
    }
}