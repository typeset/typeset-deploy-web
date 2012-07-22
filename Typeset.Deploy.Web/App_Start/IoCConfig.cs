using System.Web.Mvc;
using TinyIoC;
using Typeset.Deploy.Domain.GitHub;
using Typeset.Deploy.Web.Controllers;
using Typeset.Deploy.Web.Controllers.DependencyResolvers;
using Typeset.Deploy.Web.Controllers.Factories;

namespace Typeset.Deploy.Web
{
    public class IoCConfig
    {
        public static TinyIoCContainer RegisterDependencies()
        {
            var container = new TinyIoCContainer();

            //Mvc Framework
            container.Register<IControllerFactory, TinyIocControllerFactory>();
            container.Register<System.Web.Http.Dependencies.IDependencyResolver, TinyIocDependencyResolver>();

            //Mvc Controllers
            container.Register<IController, HomeController>("Home").AsMultiInstance();
            container.Register<IController, DeployController>("Deploy").AsMultiInstance();

            //WebApi Controllers
            container.Register<ValuesController>().AsMultiInstance();

            //Domain
            container.Register<IGitHubRepository>((c, p) => new GitHubRepository("typeset", "typeset"));

            return container;
        }
    }
}