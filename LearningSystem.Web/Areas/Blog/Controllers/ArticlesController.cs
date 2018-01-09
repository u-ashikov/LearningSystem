namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Contracts;
    using Web.Infrastructure.Enums;

    using static Common.Constants.WebConstants;

    public class ArticlesController : BlogController
    {
		private readonly IBlogAuthorArticleService articles;

		private readonly UserManager<User> userManager;

		public ArticlesController(IBlogAuthorArticleService articles, UserManager<User> userManager)
		{
			this.articles = articles;
			this.userManager = userManager;
		}

		[Route(Routing.AddArticle)]
		public IActionResult Add() => View();

		[HttpPost]
        [Route(Routing.AddArticle)]
        [ValidateAntiForgeryToken]
		public IActionResult Add(ArticleFormModel article)
		{
			if (!ModelState.IsValid)
			{
				return View(article);
			}

			string authorId = this.userManager.GetUserId(HttpContext.User);

			this.articles.Add(authorId, article.Title, article.Content);

			this.GenerateMessage(SuccessArticleCreation, Alert.Success);

			return RedirectToRoute(Routing.AllArticles);
		}
	}
}
