using System;
using System.Web.Mvc;
using TinyIoC;

namespace Typeset.Deploy.Web.Controllers.Factories
{
    public class TinyIocControllerFactory : DefaultControllerFactory, IDisposable
    {
        private TinyIoCContainer Container { get; set; }

        public TinyIocControllerFactory(TinyIoCContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }

        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            IController resolvedType = null;
            Container.TryResolve<IController>(controllerName, out resolvedType);
            return resolvedType;
        }

        public override void ReleaseController(IController controller)
        {
            if (controller is IDisposable)
            {
                (controller as IDisposable).Dispose();
            }
        }

        #region Disposable Member(s)

        private bool Disposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    Container.Dispose();
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                Disposed = true;
            }
        }

        #endregion Disposable Member(s)
    }
}