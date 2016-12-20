using System.Web;
using System.Web.Optimization;

namespace CarApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/app").Include("~/Scripts/app/core/angular/angular.js",
                "~/Scripts/app/core/angular-route/angular-route.js",
                "~/Scripts/app/core/angular-resource/angular-resource.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/app.config.js"));
            bundles.Add(new StyleBundle("~/Content/style").Include("~/Content/app.css"));

            bundles.Add(new ScriptBundle("~/Scripts/feed-module").Include("~/Scripts/app/services/*.js",
                "~/Scripts/app/components/*.js"));

            bundles.Add(new StyleBundle("~/Content/feed-style").Include("~/Scripts/app/components/*.css"));
        }
    }
}
