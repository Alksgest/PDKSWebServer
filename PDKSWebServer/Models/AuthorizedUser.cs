﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PDKSWebServer.Models
{
    public class AuthorizedUser
    {
        [Key]
        public Int32 Id { get; set; }
        public User User { get; set; }
        public DateTime AuthExpirationTime { get; set; }
    }
}
