namespace KnowledgeCenter.Data.Core.Interfaces
{
    using KnowledgeCenter.Data.Core.Models;

    /// <summary>
    /// Represents the Unit Of Work pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the KnowledgeBase Items.
        /// </summary>
        IRepository<KbItem> KbItems { get; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Initializes this Database.
        /// </summary>
        void Initialize();
    }
}
