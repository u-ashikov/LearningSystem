namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using Common.Constants;
    using System.ComponentModel.DataAnnotations;

    public class ArticleFormModel
    {
		[Required]
		[StringLength(DataConstants.Article.TitleMaxLength, MinimumLength = DataConstants.Article.TitleMinLength, ErrorMessage = DataConstants.Error.ArticleTitleLength)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }
	}
}
