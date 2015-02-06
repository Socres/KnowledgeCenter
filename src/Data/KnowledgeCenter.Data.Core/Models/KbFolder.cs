namespace KnowledgeCenter.Data.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using KnowledgeCenter.Data.Core.Models.Base;
    using KnowledgeCenter.Data.Core.Properties;

    public class KbFolder : IdentityModelBase
    {
        [SuppressMessage("ReSharper", "DoNotCallOverridableMethodsInConstructor")]
        public KbFolder()
        {
            ChildFolders = new List<KbFolder>();
            KbItems = new List<KbItem>();
        }

        [Display(Name = "KbFolder_Name_DisplayLabel", ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(250, ErrorMessageResourceName = "StringLength_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        public int? ParentFolderId { get; set; }
        [ForeignKey("ParentFolderId")]
        public virtual ICollection<KbFolder> ChildFolders { get; set; }

        public virtual ICollection<KbItem> KbItems { get; set; }
    }
}
