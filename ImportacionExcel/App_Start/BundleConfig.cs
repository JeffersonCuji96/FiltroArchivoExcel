using System.Web;
using System.Web.Optimization;

namespace ImportacionExcel
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/index").Include(
                      "~/Content/index.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                  "~/Scripts/datatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatablescss").Include(
                        "~/Content/datatables.css"));
        }
    }
}
