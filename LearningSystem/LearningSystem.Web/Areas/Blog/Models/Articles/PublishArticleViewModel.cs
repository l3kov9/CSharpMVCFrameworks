namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstrants;

    public class PublishArticleViewModel
    {
        [Required]
        [MinLength(ArticleTitleMinLength)]
        [MaxLength(ArticleTitleMaxLength)]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
