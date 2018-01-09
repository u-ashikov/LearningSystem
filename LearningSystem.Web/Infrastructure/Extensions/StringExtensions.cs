namespace LearningSystem.Web.Infrastructure.Extensions
{
	using Common.Constants;

	public static class StringExtensions
    {
		public static string CutArticleContent(this string content)
		{
			if (string.IsNullOrWhiteSpace(content) || content.Length <= WebConstants.ArticleContentMinLength)
			{
				return content;
			}

			return content.Substring(0, WebConstants.ArticleContentMinLength) + "...";
		}
    }
}
