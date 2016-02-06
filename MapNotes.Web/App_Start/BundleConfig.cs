using System.Web.Optimization;

namespace MapNotes.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js"
            ));

            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/bootstrap.css",
                "~/Content/site.css"
            ));
        }
    }
}
