namespace LearningSystem.Web.Models.Courses
{
	using Services.Models.Course;
	using System.Collections.Generic;

	public class AllCoursesViewModel
    {
		public IEnumerable<CourseListingServiceModel> Courses { get; set; }

		public PaginationViewModel Pagination { get; set; }
    }
}
