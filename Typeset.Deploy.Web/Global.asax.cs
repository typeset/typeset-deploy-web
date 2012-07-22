using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Typeset.Deploy.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IoCConfig.RegisterDependencies();

            MvcHandler.DisableMvcResponseHeader = true;
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ControllerFactoryConfig.SetControllerFactory(container.Resolve<IControllerFactory>());
            DependecyResolverConfig.RegisterDependencyResolver(container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}