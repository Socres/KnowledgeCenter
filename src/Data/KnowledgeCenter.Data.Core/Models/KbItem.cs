namespace KnowledgeCenter.Data.Core.Models
{
    using System.ComponentModel.DataAnnotations;
    using KnowledgeCenter.Data.Core.Models.Base;
    using KnowledgeCenter.Data.Core.Properties;

    public class KbItem : IdentityModelBase
    {
        [Display(Name = "KbItem_Name_DisplayLabel", ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(250, ErrorMessageResourceName = "StringLength_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(Name = "KbItem_Markdown_DisplayLabel", ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public string Markdown { get; set; }

        [Required(ErrorMessageResourceName = "Required_ValidationError", ErrorMessageResourceType = typeof(Resources))]
        public int KbFolderId { get; set; }
        public virtual KbFolder KbFolder { get; set; }
    }
}
