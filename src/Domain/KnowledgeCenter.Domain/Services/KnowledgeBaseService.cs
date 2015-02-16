namespace KnowledgeCenter.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnowledgeCenter.Data.Core.Interfaces;
    using KnowledgeCenter.Domain.Core.Interfaces;
    using KnowledgeCenter.Domain.Core.Models;

    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeBaseService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public KnowledgeBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<KnowledgeBaseItem> GetItems(Guid? parentId)
        {
            var kbItems = _unitOfWork.KbItems.Fetch(
                f => f.Name,
                f => f.Parent.DomainId == parentId);

            return kbItems.Select(KnowledgeBaseItem.FromKbItem).ToList();
        }

        /// <summary>
        /// Gets the tree from the child to the root parent, including all childs of each parent
        /// </summary>
        /// <param name="childId">The child identifier.</param>
        /// <returns></returns>
        public KnowledgeBaseItem GetRootTreeFromChild(Guid childId)
        {
            var item = _unitOfWork.KbItems.Fetch(i => i.DomainId == childId).Single();
            // Get all parents
            while (item.ParentItemId.HasValue)
            {
                item = _unitOfWork.KbItems.GetById(item.ParentItemId.Value);
                if (item.ParentItemId.HasValue)
                {
                    // Get all items that share this parent
                    var parentItemId = item.ParentItemId.Value;
                    var childs = _unitOfWork.KbItems.Fetch(
                        i => i.Name,
                        i => i.ParentItemId == parentItemId)
                        .ToList();
                    item = childs.First();
                }
            }

            return KnowledgeBaseItem.FromKbItem(item);
        }
    }
}
