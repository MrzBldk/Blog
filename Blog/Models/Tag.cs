using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Tag
    {
        [Key]
        public string TagText { get; set; }

        public List<Article> Articles { get; set; } = new();
    }
}
