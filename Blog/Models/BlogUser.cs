using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace Blog.Models
{
    public class BlogUser : IdentityUser
    {
        public List<Article> Articles { get; set; } = new();
    }
}
