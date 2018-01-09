namespace LearningSystem.Web.Models.Home
{
    using Services.Models.Course;
    using Services.Models.User;
    using System.Collections.Generic;

    public class SearchResultsViewModel
	{
		public string SearchTerm { get; set; }

		public IEnumerable<UserSearchServiceModel> Users { get; set; }

		public IEnumerable<CourseSearchServiceModel> Courses { get; set; }
	}
}
