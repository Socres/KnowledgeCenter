namespace KnowledgeCenter.Domain.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using KnowledgeCenter.Domain.Core.Models;

    public interface IKnowledgeBaseService
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KnowledgeBaseItem> GetItems(Guid? parentId);

        /// <summary>
        /// Gets the root tree from the given child.
        /// </summary>
        /// <param name="childId">The child identifier.</param>
        /// <returns></returns>
        KnowledgeBaseItem GetRootTreeFromChild(Guid childId);
    }
}
