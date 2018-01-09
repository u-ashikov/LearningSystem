namespace LearningSystem.Web.Models.Courses
{
	using Services.Models.Course;

	public class CourseDetailsViewModel
    {
		public CourseDetailsServiceModel Course { get; set; }

		public bool IsUserInCourse { get; set; }
    }
}
