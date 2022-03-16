using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Blog.Models
{
    public class BlogUser : IdentityUser
    {
        public List<Article> Articles { get; set; } = new();
    }
}
