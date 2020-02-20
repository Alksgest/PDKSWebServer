using System.ComponentModel.DataAnnotations;

namespace PdksPersistence.Models
{
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        //public virtual ICollection<ArticleCategory> Categories { get; set; }
    }
}
