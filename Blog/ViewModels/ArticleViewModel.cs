using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Blog.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        public string Tags { get; set; }

        public bool CheckMeAsAuthor { get; set; }
    }
}
