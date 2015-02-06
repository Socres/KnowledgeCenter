namespace KnowledgeCenter.Domain.Services
{
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
        /// Gets the folders.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<KnowledgeBaseFolder> GetFolders(int? parentId)
        {
            var kbFolders = _unitOfWork.KbFolders.Fetch(
                f => f.ParentFolderId == parentId, 
                f => f.ChildFolders);

            return kbFolders.Select(KnowledgeBaseFolder.FromKbFolder).ToList();
        }
    }
}
