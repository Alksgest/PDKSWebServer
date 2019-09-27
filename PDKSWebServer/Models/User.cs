using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDKSWebServer.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Username")]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Password")]
        public string Password { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Role")]
        public string Role { get; set; }
    }
}
