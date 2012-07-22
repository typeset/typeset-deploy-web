using System.Web.Mvc;

namespace Typeset.Deploy.Web
{
    public class ControllerFactoryConfig
    {
        public static void SetControllerFactory(IControllerFactory controllerFactory)
        {
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}