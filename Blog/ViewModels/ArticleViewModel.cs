using System.ComponentModel.DataAnnotations;


namespace Blog.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        public string Tags { get; set; }

        [Display(Name = "Check me as author?")]
        public bool CheckMeAsAuthor { get; set; }
    }
}
