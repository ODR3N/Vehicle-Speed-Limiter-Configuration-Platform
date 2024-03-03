using System.Web;
using System.Web.Optimization;

namespace PModuloLimitadorV
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // estilos personalizados
            bundles.Add(new StyleBundle("~/Content/mycssLogin").Include(
                      "~/Content/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/mycss").Include(
                      "~/Content/css/general.css",
                      "~/Content/css/navbar.css",
                      "~/Content/css/footer.css"));

            bundles.Add(new StyleBundle("~/Content/myDetailsCss").Include(
                      "~/Content/css/general.css",
                      "~/Content/css/details.css",
                      "~/Content/css/navbar.css",
                      "~/Content/css/footer.css"));

            bundles.Add(new StyleBundle("~/Content/mycssIndex").Include(
                      "~/Content/css/index.css"));

            bundles.Add(new StyleBundle("~/Content/cssIndexHome").Include(
                      "~/Content/css/indexHome.css"));
        }
    }
}
