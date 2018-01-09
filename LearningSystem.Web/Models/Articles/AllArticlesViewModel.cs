namespace LearningSystem.Web.Models.Articles
{
	using Services.Models.Article;
	using System.Collections.Generic;

	public class AllArticlesViewModel
    {
		public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

		public PaginationViewModel Pagination { get; set; }
    }
}
