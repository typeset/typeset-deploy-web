using System.Web;
using System.Web.Optimization;

namespace Typeset.Deploy.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssBundle = new Bundle("~/content/css", new CssMinify());
            cssBundle.IncludeDirectory("~/Content", "typeset*.css", true);
            bundles.Add(cssBundle);

            var jsBundle = new Bundle("~/scripts/js/all", new JsMinify());
            jsBundle.IncludeDirectory("~/Scripts", "*min.js", true);
            jsBundle.IncludeDirectory("~/Scripts", "typeset*.js", true);
            bundles.Add(jsBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}