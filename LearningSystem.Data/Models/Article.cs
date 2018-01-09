namespace LearningSystem.Data.Models
{
    using Common.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
		public int Id { get; set; }

		[Required]
		[StringLength(DataConstants.Article.TitleMaxLength,MinimumLength = DataConstants.Article.TitleMinLength,ErrorMessage = DataConstants.Error.ArticleTitleLength)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }

		public DateTime PublishDate { get; set; }

		public string AuthorId { get; set; }

		public User Author { get; set; }
    }
}
