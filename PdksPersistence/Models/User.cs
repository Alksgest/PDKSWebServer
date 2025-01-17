﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdksPersistence.Models
{
    public partial class User : EntityBase
    {
        [Required]
        [MaxLength(20)]
        public String Username { get; set; }
        [Required]
        [MaxLength(20)]
        public String Password { get; set; }
        public String Firstname { get; set; }
        public String Surname { get; set; }
        [Required]
        [MaxLength(20)]
        public UserRole Role { get; set; }
    }
}
