namespace LearningSystem.Services.Blog
{
    using Blog.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogArticleService
    {
        Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1);

        Task CreateAsync(string title, string content, string authorId);

        int Total();

        Task<ArticleDetailsServiceModel> ByIdAsync(int id);
    }
}
