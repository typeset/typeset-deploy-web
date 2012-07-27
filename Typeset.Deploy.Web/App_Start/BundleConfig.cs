using System.Web;
using System.Web.Optimization;

namespace Typeset.Deploy.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssBundle = new Bundle("~/content/css", new CssMinify());
            cssBundle.Include("~/content/bootstrap.min.css");
            cssBundle.Include("~/content/bootstrap-custom.css");
            cssBundle.Include("~/content/bootstrap-responsive.min.css");
            cssBundle.IncludeDirectory("~/content", "typeset*.css", true);
            bundles.Add(cssBundle);

            var jsBundle = new Bundle("~/scripts/js/all", new JsMinify());
            jsBundle.Include("~/scripts/bootstrap.min.js");
            jsBundle.IncludeDirectory("~/scripts", "typeset*.js", true);
            bundles.Add(jsBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}