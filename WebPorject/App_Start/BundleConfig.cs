using System.Web;
using System.Web.Optimization;

namespace WebPorject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/lib/jquery/jquery.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/lib/bootstrap-rtl/css/bootstrap.min.css",
                       "~/lib/bootstrap-rtl/css/bootstrap-theme.min.css",
                       "~/lib/bootstrap-rtl/css/bootstrap-rtl.min.css",
                       "~/lib/bootstrap-rtl/css/costum.css"));
        }
        }
}
