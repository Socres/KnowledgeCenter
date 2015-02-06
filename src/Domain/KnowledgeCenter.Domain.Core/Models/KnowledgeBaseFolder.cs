namespace KnowledgeCenter.Domain.Core.Models
{
    using System;
    using System.Collections.Generic;
    using KnowledgeCenter.Data.Core.Models;
    using KnowledgeCenter.Domain.Core.Models.Base;

    public class KnowledgeBaseFolder : DomainModelBase
    {
        private readonly List<KnowledgeBaseFolder> _childFolders;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeBaseFolder" /> class.
        /// </summary>
        /// <param name="domainId">The domain identifier.</param>
        /// <param name="name">The name.</param>
        public KnowledgeBaseFolder(Guid domainId, string name)
        {
            DomainId = domainId;
            Name = name;
            _childFolders = new List<KnowledgeBaseFolder>();
        }

        /// <summary>
        /// Create a new <see cref="KnowledgeBaseFolder"/> from the given <see cref="KbFolder"/>.
        /// </summary>
        /// <param name="kbFolder">The kb folder.</param>
        /// <returns></returns>
        public static KnowledgeBaseFolder FromKbFolder(KbFolder kbFolder)
        {
            var result = new KnowledgeBaseFolder(kbFolder.DomainId, kbFolder.Name);

            foreach (var childFolder in kbFolder.ChildFolders)
            {
                result._childFolders.Add(FromKbFolder(childFolder));
            }

            return result;
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<KnowledgeBaseFolder> ChildFolders
        {
            get { return _childFolders.AsReadOnly(); }
        }
    }
}
