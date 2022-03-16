using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class BlogContext : IdentityDbContext<BlogUser>
    {

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
