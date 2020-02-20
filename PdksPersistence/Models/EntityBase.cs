using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PdksPersistence.Models
{
    public class EntityBase
    {
        [Key/*, DatabaseGenerated(DatabaseGeneratedOption.Identity)*/]
        public int Id { get; set; }
    }
}
