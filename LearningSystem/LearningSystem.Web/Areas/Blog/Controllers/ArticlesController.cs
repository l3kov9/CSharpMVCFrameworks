namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Helpers;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services;
    using Services.Blog;
    using Services.Html;
    using System;
    using System.Threading.Tasks;

    [Area(WebConstants.BlogArea)]
    [Authorize(Roles = WebConstants.BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IBlogArticleService articles;
        private readonly IHtmlService html;

        public ArticlesController(UserManager<User> userManager, IBlogArticleService articles, IHtmlService html)
        {
            this.userManager = userManager;
            this.articles = articles;
            this.html = html;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var articles = await this.articles.AllAsync(page);
            var totalPages = (int)Math.Ceiling((double)this.articles.Total() / ServiceConstants.BlogArticlePageSize);

            return View(new ArticleListingViewModel
            {
                Articles = articles,
                CurrentPage = page,
                TotalPages = totalPages
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.articles.ByIdAsync(id));

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublishArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.articles.CreateAsync(model.Title, model.Content, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
