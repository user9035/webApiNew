using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace CarApp.Web.Utility
{
    internal sealed class UnityResolver : IDependencyResolver
    {
        private IUnityContainer container;

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        internal UnityResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException();
            this.container = container;
        }

        public void Dispose()
        {
            if (this.container == null)
                return;
            this.container.Dispose();
            this.container = null;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}
