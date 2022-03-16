using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext _blogcontext;
        public HomeController(BlogContext blogContext)
        {
            _blogcontext = blogContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Articles()
        //{
        //    return View(await _blogcontext.Articles.Include(x => x.Tags).Include(x => x.BlogUser).ToListAsync());
        //}

        [HttpGet]
        public async Task<IActionResult> Articles(DateTime start, DateTime end, string category, string tags)
        {

            return View(await _blogcontext.Articles.Include(x => x.Tags).Include(x => x.BlogUser)
                .Where(x => (x.PublicationTime >= start) &&
                (x.PublicationTime <= (end == DateTime.MinValue ? DateTime.MaxValue : end)) &&
                (category == null || x.Category.ToLower() == category.ToLower()) &&
                (tags == null || x.Tags.Any(y => tags.ToLower().Contains(y.TagText.ToLower()))))
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Article(int id)
        {
            return View(await _blogcontext.Articles.Include(x => x.BlogUser).FirstOrDefaultAsync(x => x.ArticleId == id));
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleViewModel model, [FromForm] IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                
                if (uploadedFile != null)
                {
                    MemoryStream ms = new();

                    uploadedFile.CopyTo(ms);
                    imageData = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }

                string[] tags = model.Tags.Split(' ');
                List<Tag> tagsList = new();
                foreach(var tag in tags)
                {
                    tagsList.Add(new() { TagText = tag });
                }

                Article article = new()
                {
                    Name = model.Name,
                    ShortDescription = model.ShortDescription,
                    Description = model.Description,
                    HeroImage = imageData,
                    Category = model.Category,
                    BlogUserId = model.CheckMeAsAuthor ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value : null,
                    Tags = tagsList,
                    PublicationTime = DateTime.Now
                };

                _blogcontext.Articles.Add(article);
                await _blogcontext.SaveChangesAsync();

                return RedirectToAction("Articles");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            Article article = await _blogcontext.Articles.Include(x => x.Tags).FirstAsync(x => x.ArticleId == id);
            string tags = "";

            foreach (var tag in article.Tags)
                tags += $"{tag.TagText} ";

            return View(new ArticleViewModel 
            {
                Id = id, 
                Description = article.Description, 
                Category = article.Category, 
                ShortDescription = article.ShortDescription,
                Tags = tags
            }); 
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleViewModel model)
        {
            Article article = await _blogcontext.Articles.FindAsync(model.Id);

            article.Description = model.Description;
            article.Category = model.Category;
            article.ShortDescription = model.ShortDescription;

            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                string[] tags = model.Tags.Split(' ');
                foreach (var tag in tags)
                    article.Tags.Add(new() { TagText = tag });
            }
            _blogcontext.Articles.Update(article);
            await _blogcontext.SaveChangesAsync();

            return RedirectToAction("Articles");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
