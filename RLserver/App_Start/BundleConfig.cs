using System.Web;
using System.Web.Optimization;

namespace RLserver
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                       "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/datatables/jquery.dataTables.js",
                        "~/Scripts/datatables/dataTables.bootstrap4.js"
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/content/datatables/css/datatables.bootstrap4.css",
                      "~/content/fontawesome.css",
                      "~/Content/site.css"));
        }
    }
}
