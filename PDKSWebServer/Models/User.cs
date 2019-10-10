﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDKSWebServer.Models
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
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
