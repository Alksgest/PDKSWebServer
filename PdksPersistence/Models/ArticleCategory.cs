using System.ComponentModel.DataAnnotations;

namespace PdksPersistence.Models
{
    public class ArticleCategory : EntityBase
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
