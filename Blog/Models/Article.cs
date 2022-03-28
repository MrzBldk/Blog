using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public byte[] HeroImage {get;set;} 
        public string Category { get; set; }
        public DateTime PublicationTime { get; set; }

        public List<Tag> Tags { get; set; } = new();

        public BlogUser BlogUser { get; set; }
        public string BlogUserId { get; set; }
    }
}
