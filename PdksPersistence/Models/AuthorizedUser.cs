using System;
using System.ComponentModel.DataAnnotations;

namespace PdksPersistence.Models
{
    public class AuthorizedUser : EntityBase
    {
        public User User { get; set; }
        public DateTime AuthExpirationTime { get; set; }
    }
}
