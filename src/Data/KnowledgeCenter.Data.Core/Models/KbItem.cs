namespace KnowledgeCenter.Data.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using KnowledgeCenter.Data.Core.Models.Base;
    using KnowledgeCenter.Data.Core.Properties;

    public class KbItem : IdentityModelBase
    {
        public KbItem()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            ChildItems = new List<KbItem>();
        }

        [Display(Name = "KbItem_Name_DisplayLabel", ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(250, ErrorMessageResourceName = "StringLength_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(Name = "KbItem_Markdown_DisplayLabel", ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Markdown { get; set; }

        public int? ParentItemId { get; set; }
        public virtual KbItem Parent { get; set; }

        [ForeignKey("ParentItemId")]
        public virtual ICollection<KbItem> ChildItems { get; set; }
    }
}
