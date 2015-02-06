namespace KnowledgeCenter.Web.AppStart
{
    using KnowledgeCenter.Data.Core.Interfaces;
    using KnowledgeCenter.Framework.DependencyInjection;

    /// <summary>
    /// Class for startup functionality.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initializes the system.
        /// </summary>
        public void InitializeSystem()
        {
            var unitOfWork = IoC.Instance.Resolve<IUnitOfWork>();
            unitOfWork.Initialize();
        }
    }
}