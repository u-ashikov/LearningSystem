namespace LearningSystem.Services.Models.Course
{
    using Data.Models;
    using Infrastructure.Automapper;

    public class CourseDetailsServiceModel : CourseServiceModel,IMapFrom<Course>
    {
		public int Id { get; set; }

		public string Description { get; set; }

		public string TrainerName { get; set; }
	}
}
