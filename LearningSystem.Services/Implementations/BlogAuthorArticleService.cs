namespace LearningSystem.Services.Implementations
{
    using Contracts;
    using Data.Models;
    using System;
    using Web.Data;

    public class BlogAuthorArticleService : IBlogAuthorArticleService
	{
		private readonly LearningSystemDbContext db;

		public BlogAuthorArticleService(LearningSystemDbContext db)
		{
			this.db = db;
		}

		public void Add(string authorId, string title, string content)
		{
			var article = new Article()
			{
				Title = title,
				Content = content,
				PublishDate = DateTime.Now,
				AuthorId = authorId
			};

			this.db.Articles.Add(article);
			this.db.SaveChanges();
		}
	}
}
