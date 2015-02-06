namespace KnowledgeCenter.DI
{
    using KnowledgeCenter.Data;
    using KnowledgeCenter.Data.Core.Interfaces;
    using KnowledgeCenter.Domain.Core.Interfaces;
    using KnowledgeCenter.Domain.Services;
    using KnowledgeCenter.Framework.DependencyInjection;

    public class IoCBuilder
    {
        public void Initialize(IoC ioc)
        {
            ioc.Register<IUnitOfWork, UnitOfWork>().PerLifetimeScope();
            ioc.Register<IKnowledgeBaseService, KnowledgeBaseService>().PerLifetimeScope();            
        }
    }
}
