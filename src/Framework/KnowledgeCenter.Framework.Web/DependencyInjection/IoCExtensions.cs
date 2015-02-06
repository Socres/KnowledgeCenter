namespace KnowledgeCenter.Framework.Web.DependencyInjection
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using KnowledgeCenter.Framework.DependencyInjection;

    /// <summary>
    /// Extensions for IoC
    /// </summary>
    public static class IoCExtensions
    {
        /// <summary>
        /// Registers the MVC.
        /// </summary>
        /// <param name="ioc">The ioc.</param>
        /// <param name="assembly">The assembly.</param>
        public static void RegisterMvc(this IoC ioc, Assembly assembly)
        {
            ioc.Builder.RegisterControllers(assembly);
            ioc.Builder.RegisterModelBinders(assembly);
            ioc.Builder.RegisterModelBinderProvider();
            ioc.Builder.RegisterModule<AutofacWebTypesModule>();
        }

        /// <summary>
        /// Registers the web API.
        /// </summary>
        /// <param name="ioc">The ioc.</param>
        /// <param name="assembly">The assembly.</param>
        public static void RegisterWebApi(this IoC ioc, Assembly assembly)
        {
            ioc.Builder.RegisterApiControllers(assembly);
        }
    }
}
