namespace KnowledgeCenter.Framework.Web.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using KnowledgeCenter.Framework.DependencyInjection;

    public class IoCWebApiDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">The type of the requested service or object.</param>
        /// <returns>
        /// The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            return
                IoC.Instance.IsRegistered(serviceType)
                    ? IoC.Instance.Resolve(serviceType)
                    : null;
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>
        /// The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            if (IoC.Instance.IsRegistered(enumerableServiceType))
            {
                var instance = IoC.Instance.Resolve(enumerableServiceType);
                return (IEnumerable<object>)instance;
            }

            return new List<object>();
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDependencyScope BeginScope()
        {
            return null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
