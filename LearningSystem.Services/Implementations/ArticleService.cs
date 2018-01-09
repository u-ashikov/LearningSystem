namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Models.Article;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Data;

    public class ArticleService : IArticleService
    {
		private readonly LearningSystemDbContext db;

		public ArticleService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public IEnumerable<ArticleListingServiceModel> All(int page, string searchTerm, int pageSize = 10)
		{
			var articles = this.db.Articles.AsQueryable();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				articles = articles.Where(a=>a.Title.ToLower().Contains(searchTerm.ToLower()) || a.Content.ToLower().Contains(searchTerm.ToLower())).AsQueryable();
			}

			return articles
							.OrderByDescending(a => a.Id)
							.Skip((page - 1) * pageSize)
							.Take(pageSize)
							.ProjectTo<ArticleListingServiceModel>();
		}		

		public BaseArticleServiceModel GetArticleById(int id) =>
			this.db.Articles
				.Where(a => a.Id == id)
				.ProjectTo<BaseArticleServiceModel>()
				.FirstOrDefault();

		public bool ArticleExists(int id) => this.db.Articles.Any(a => a.Id == id);

		public int TotalArticles(string searchTerm)
		{
			if (!string.IsNullOrEmpty(searchTerm))
			{
				return this.db.Articles.Count(a => a.Title.ToLower().Contains(searchTerm.ToLower()) || a.Content.ToLower().Contains(searchTerm.ToLower()));
			}

			return this.db.Articles.Count();
		}
	}
}
