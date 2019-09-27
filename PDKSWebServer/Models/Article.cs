using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDKSWebServer.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Title")]
        public string Title { get; set; }
        [Required]
        [Column("Author")]
        public User Author { get; set; }
        [Required]
        [Column("Categories")]
        public List<Category> Categories { get; set; }
        [Required]
        [Column("Content")]
        public string Content { get; set; }
        [Required]
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
