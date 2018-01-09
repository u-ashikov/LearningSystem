namespace LearningSystem.Services.Contracts
{
	public interface IBlogAuthorArticleService
    {
		void Add(string authorId, string title, string content);
	}
}
