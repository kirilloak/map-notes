using System.Web.Optimization;

namespace MapNotes.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/layout.css",
                "~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/thirdparty/markerwithlabel.js"
            ));

            bundles.Add(new ScriptBundle("~/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/app/app.js"
            ));
        }
    }
}
