using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Dtos
{
    public class ArticleDto
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public UserDto Author { get; set; }
        //public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public CategoryDto Category { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
