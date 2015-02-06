namespace KnowledgeCenter.Domain.Core.Models.Base
{
    using System;

    /// <summary>
    /// Base class for domain models with a DomainId.
    /// </summary>
    public abstract class DomainModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid DomainId { get; set; }
    }
}