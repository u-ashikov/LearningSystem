namespace LearningSystem.Web.Controllers
{
    using Infrastructure.Enums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Articles;
    using Services.Contracts;

    using static Common.Constants.WebConstants;

    [Authorize]
	[Route(Routing.BlogArticles)]
	public class ArticlesController : BaseController
    {
        private const string AllArticlesTemplate = "all";

		private readonly IArticleService articles;

		public ArticlesController(IArticleService articles)
		{
			this.articles = articles;
		}

		[Route(AllArticlesTemplate, Name = Routing.AllArticles)]
		public IActionResult All(string searchTerm, int page = 1)
        {
            if (page < MinPageSize)
            {
                return RedirectToAction(nameof(All), new { searchTerm, page = MinPageSize });
            }

            var pagination = new PaginationViewModel()
            {
                Action = nameof(All),
                SearchTerm = searchTerm,
                PageSize = ArticlesPageSize,
                CurrentPage = page,
                TotalElements = this.articles.TotalArticles(searchTerm)
            };

            if (page > pagination.TotalPages && pagination.TotalPages != 0)
            {
                return RedirectToAction(nameof(All), new { searchTerm, page = pagination.TotalPages});
            }

            return View(new AllArticlesViewModel()
            {
                Articles = this.articles.All(page, searchTerm, ArticlesPageSize),
                Pagination = pagination
            });
        }

		[Route(Routing.ArticleDetails)]
		public IActionResult Details(int id)
		{
			if (!this.articles.ArticleExists(id))
			{
				this.GenerateMessage(string.Format(NonExistingArticle, id), Alert.Warning);

				return RedirectToAction(nameof(All));
			}

			return View(this.articles.GetArticleById(id));
		}
	}
}
