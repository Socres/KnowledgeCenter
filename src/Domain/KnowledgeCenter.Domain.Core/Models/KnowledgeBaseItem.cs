namespace KnowledgeCenter.Domain.Core.Models
{
    using System;
    using System.Collections.Generic;
    using KnowledgeCenter.Data.Core.Models;
    using KnowledgeCenter.Domain.Core.Models.Base;

    public class KnowledgeBaseItem : DomainModelBase
    {
        private readonly List<KnowledgeBaseItem> _childItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeBaseItem" /> class.
        /// </summary>
        /// <param name="domainId">The domain identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="markdown">The markdown.</param>
        public KnowledgeBaseItem(Guid domainId, string name, string markdown)
        {
            DomainId = domainId;
            Name = name;
            Markdown = markdown;
            _childItems = new List<KnowledgeBaseItem>();
        }

        /// <summary>
        /// Create a new <see cref="KnowledgeBaseItem"/> from the given <see cref="KbItem"/>.
        /// </summary>
        /// <param name="kbItem">The kb item.</param>
        /// <returns></returns>
        public static KnowledgeBaseItem FromKbItem(KbItem kbItem)
        {
            var result = new KnowledgeBaseItem(kbItem.DomainId, kbItem.Name, kbItem.Markdown);

            foreach (var childItem in kbItem.ChildItems)
            {
                result._childItems.Add(FromKbItem(childItem));
            }

            return result;
        }

        public string Name { get; private set; }

        public string Markdown { get; private set; }

        public IReadOnlyCollection<KnowledgeBaseItem> ChildItems
        {
            get { return _childItems.AsReadOnly(); }
        }
    }
}
