namespace KnowledgeCenter.Data.Core.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using KnowledgeCenter.Data.Core.Properties;

    /// <summary>
    /// Base class for domain models with a DomainId.
    /// </summary>
    public abstract class IdentityModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Required(ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public Guid DomainId { get; set; }
    }
}