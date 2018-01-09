namespace LearningSystem.Web.Models.Trainers
{
	using Services.Models.Trainer;
	using System.Collections.Generic;

	public class TrainerCoursesViewModel
    {
		public IEnumerable<TrainerCourseListingServiceModel> Courses { get; set; }

		public PaginationViewModel Pagination { get; set; }
    }
}
