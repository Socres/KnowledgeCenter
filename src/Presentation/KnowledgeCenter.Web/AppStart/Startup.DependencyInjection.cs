namespace KnowledgeCenter.Web.AppStart
{
    using System.Web.Http;
    using System.Web.Mvc;
    using KnowledgeCenter.DI;
    using KnowledgeCenter.Framework.DependencyInjection;
    using KnowledgeCenter.Framework.Web.DependencyInjection;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration;
    using KnowledgeCenter.Web.Infrastructure.JsResources;
    using Owin;

    /// <summary>
    /// Class for configuring IOC.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the unity Dependency Injection.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="config">The configuration.</param>
        public void SetupIoC(IAppBuilder app, HttpConfiguration config)
        {
            var ioCBuilder = new IoCBuilder();
            ioCBuilder.Initialize(IoC.Instance);

            IoC.Instance.Register<IJsResourcesProvider, JsResourcesProvider>().PerLifetimeScope();
            IoC.Instance.Register<IClientConfigProvider, ClientConfigProvider>().PerLifetimeScope();

            IoC.Instance.RegisterMvc(GetType().Assembly);
            IoC.Instance.RegisterWebApi(GetType().Assembly);

            IoC.Instance.Build();

            DependencyResolver.SetResolver(new IoCMvcDependencyResolver());
            config.DependencyResolver = new IoCWebApiDependencyResolver();

            app.UseIoCMiddleware(config);
        }
    }
}