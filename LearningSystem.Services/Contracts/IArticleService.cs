namespace LearningSystem.Services.Contracts
{
    using Models.Article;
    using System.Collections.Generic;

    public interface IArticleService
    {
		IEnumerable<ArticleListingServiceModel> All(int page, string searchTerm, int pageSize = 10);

		BaseArticleServiceModel GetArticleById(int id);

		bool ArticleExists(int id);

		int TotalArticles(string searchTerm);
    }
}
