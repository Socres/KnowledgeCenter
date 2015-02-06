namespace KnowledgeCenter.Domain.Core.Interfaces
{
    using System.Collections.Generic;
    using KnowledgeCenter.Domain.Core.Models;

    public interface IKnowledgeBaseService
    {
        /// <summary>
        /// Gets the folders.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KnowledgeBaseFolder> GetFolders(int? parentId);
    }
}
