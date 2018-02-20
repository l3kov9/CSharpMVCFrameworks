namespace LearningSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using LearningSystem.Services.Blog.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogArticleService : IBlogArticleService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ServiceConstants.BlogArticlePageSize)
                .Take(ServiceConstants.BlogArticlePageSize)
                .ProjectTo<ArticleListingServiceModel>()
                .ToListAsync();

        public async Task<ArticleDetailsServiceModel> ByIdAsync(int id)
            => await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }

        public int Total()
            => this.db
                .Articles
                .Count();
    }
}
