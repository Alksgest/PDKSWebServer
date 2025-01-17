﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdksPersistence.Models
{
    public class Article : EntityBase
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public Category Category { get; set; }

        public UserRole? AccessLevel { get; set; }

        //public virtual ICollection<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();
    }
}
